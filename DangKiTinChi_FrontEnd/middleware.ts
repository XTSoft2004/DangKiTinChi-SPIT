import { NextRequest, NextResponse } from "next/server";
import globalConfig from "./app.config";
import { cookies, headers } from "next/headers";
import { IUser } from "./types/user";

export const config = {
  matcher: [
    /*
     * Match all request paths except for the ones starting with:
     * - api (API routes)
     * - _next/static (static files)
     * - _next/image (image optimization files)
     * - favicon.ico, sitemap.xml, robots.txt (metadata files)
     * - images (public images folder and subdirectories)
     */
    "/((?!api|_next/static|_next/image|favicon.ico|sitemap.xml|robots.txt|images).*)",
  ],
};

const getMe = async () => {
  const response = await fetch(`${globalConfig.baseUrl}/user/me`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${(await cookies()).get("accessToken")?.value}`,
    },
  });

  const data = await response.json();

  if (response.ok) {
    return {
      ok: response.ok,
      status: response.status,
      data: data.data as IUser,
    };
  }
};

const redirectTo = (url: string, request: NextRequest) => {
  return NextResponse.redirect(new URL(url, request.url));
};

export async function middleware(request: NextRequest) {
  const nextUrl = request.nextUrl.pathname;
  console.log("server >> middleware", nextUrl);

  const accessToken = (await cookies()).get("accessToken")?.value;
  
  // Định nghĩa các route cần bảo vệ (cần đăng nhập)
  const protectedRoutes = ["/", "/user", "/course"];
  // Định nghĩa các route auth (không cần đăng nhập)
  const authRoutes = ["/auth"];
  
  const isProtectedRoute = protectedRoutes.some(route => 
    nextUrl === route || nextUrl.startsWith(route + "/")
  );
  const isAuthRoute = authRoutes.some(route => 
    nextUrl === route || nextUrl.startsWith(route + "/")
  );

  // Case 1: Chưa đăng nhập
  if (!accessToken) {
    // Nếu đang truy cập route được bảo vệ -> redirect về auth
    if (isProtectedRoute) {
      console.log("User not authenticated, redirecting to /auth");
      return redirectTo("/auth", request);
    }
    // Nếu đang ở auth route -> cho phép tiếp tục
    return NextResponse.next();
  }

  // Case 2: Đã đăng nhập
  if (accessToken) {
    // Verify token bằng cách gọi API /user/me
    const userResponse = await getMe();
    
    // Nếu token không hợp lệ -> redirect về auth
    if (!userResponse?.ok) {
      console.log("Invalid token, redirecting to /auth");
      // Xóa token không hợp lệ
      const response = redirectTo("/auth", request);
      response.cookies.delete("accessToken");
      return response;
    }

    // Nếu token hợp lệ nhưng đang truy cập auth route -> redirect về home
    if (isAuthRoute) {
      console.log("User already authenticated, redirecting to /");
      return redirectTo("/", request);
    }

    // Token hợp lệ và không phải auth route -> cho phép tiếp tục
    return NextResponse.next();
  }

  return NextResponse.next();
}
