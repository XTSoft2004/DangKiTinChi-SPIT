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

// Danh sách các trang ngoại lệ không cần đăng nhập
const EXCLUDED_PATHS = ["/", "/auth"];

export async function middleware(request: NextRequest) {
  const nextUrl = request.nextUrl.pathname;
  console.log("server >> middleware", nextUrl);

  const accessToken = (await cookies()).get("accessToken")?.value;

  // Kiểm tra nếu đường dẫn nằm trong danh sách ngoại lệ thì cho phép truy cập
  const isExcluded = EXCLUDED_PATHS.some((path) =>
    nextUrl === path || nextUrl.startsWith(path + "/")
  );

  if (isExcluded) {
    // Nếu đã đăng nhập mà vào /auth thì redirect về /
    if (accessToken && nextUrl.startsWith("/auth")) {
      console.log("User already authenticated, redirecting to /");
      return redirectTo("/", request);
    }
    return NextResponse.next();
  }

  // Nếu không có accessToken => redirect về /auth
  if (!accessToken) {
    console.log("User not authenticated, redirecting to /auth");
    return redirectTo("/auth", request);
  }

  // Nếu đã đăng nhập, kiểm tra token hợp lệ
  if (accessToken) {
    const userResponse = await getMe();
    if (!userResponse?.ok) {
      console.log("Invalid token, redirecting to /auth");
      const response = redirectTo("/auth", request);
      response.cookies.delete("accessToken");
      return response;
    }
  }

  return NextResponse.next();
}
