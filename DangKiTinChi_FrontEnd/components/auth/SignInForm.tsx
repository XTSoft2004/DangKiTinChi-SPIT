'use client';

import { useForm, FormProvider } from "react-hook-form";
import { Button } from "@/components/ui/shadcn-ui/button";
import { CustomInputField } from "@/components/ui/Input/CustomInputField"; // Điều chỉnh path nếu cần
import { User, Lock } from "lucide-react"; // Icon cho username và password
import { loginAccount } from "@/actions/auth.actions";
import { ILoginRequest } from "@/types/auth";
import { toast } from "sonner";
import { getOrCreateDeviceId } from "@/utils/deviceId";
import { ToastHelper } from "@/utils/useAppToast";
import { useRouter } from "next/navigation";

export function SignInForm() {
  const router = useRouter();

  const methods = useForm<ILoginRequest>({
    defaultValues: {
      userName: "",
      password: "",
    },
  });

  const { handleSubmit } = methods;

  const onSubmit = async (data: ILoginRequest) => {
    ToastHelper.loading({
      title: "Đang đăng nhập, vui lòng đợi...",
      duration: 1000,
    });
    const deviceId = getOrCreateDeviceId();
    const loginResponse = await loginAccount({ ...data, deviceId });
    if (loginResponse.ok) {
      ToastHelper.success({
        title: "Đăng nhập thành công",
        description: "Chào mừng bạn đã quay trở lại!",
      });
      router.push("/admin/user");
    } else {
      ToastHelper.error({
        title: "Đăng nhập thất bại",
        description: loginResponse.message || "Vui lòng kiểm tra tài khoản và mật khẩu lại!",
      })
    }
  };

  return (
    <FormProvider {...methods}>
      <div className="w-full max-w-lg mx-auto space-y-0.5 sm:space-y-1 px-1">
        {/* Welcome Message */}
        <div className="text-center mb-4 sm:mb-6">
          <h2 className="text-lg sm:text-xl md:text-2xl font-bold text-slate-800 mb-2">
            Chào mừng trở lại
          </h2>
          <p className="text-sm sm:text-base text-slate-600 leading-relaxed">
            Vui lòng nhập thông tin để đăng nhập vào hệ thống
          </p>
        </div>

        {/* Form */}
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-3 sm:space-y-4">
          <div className="animate-in fade-in slide-in-from-bottom-4 duration-500 delay-100">
            <CustomInputField
              name="userName"
              label="Tên đăng nhập"
              placeholder="Nhập tên đăng nhập của bạn"
              icon={<User className="w-4 h-4 sm:w-5 sm:h-5" />}
            />
          </div>

          <div className="animate-in fade-in slide-in-from-bottom-4 duration-500 delay-200">
            <CustomInputField
              name="password"
              label="Mật khẩu"
              type="password"
              placeholder="Nhập mật khẩu của bạn"
              icon={<Lock className="w-4 h-4 sm:w-5 sm:h-5" />}
            />
          </div>

          <div className="animate-in fade-in slide-in-from-bottom-4 duration-500 delay-300 pt-2">
            <Button
              type="submit"
              className="w-full h-11 sm:h-12 text-sm sm:text-base bg-gradient-to-r from-blue-600 to-sky-600 hover:from-blue-700 hover:to-sky-700 text-white font-semibold transition-all duration-300 shadow-lg hover:shadow-xl rounded-xl transform hover:scale-[1.02] active:scale-[0.98]"
            >
              Đăng nhập vào hệ thống
            </Button>
          </div>
        </form>

        {/* Forgot Password */}
        <div className="text-center animate-in fade-in slide-in-from-bottom-4 duration-500 delay-400 pt-3">
          <a
            href="#"
            className="text-sm text-blue-600 hover:text-blue-700 font-semibold hover:underline transition-all duration-200"
          >
            Quên mật khẩu?
          </a>
        </div>

        {/* Additional Info */}
        <div className="text-center pt-4 sm:pt-6 border-t border-gray-200 animate-in fade-in slide-in-from-bottom-4 duration-500 delay-500">
          <p className="text-xs sm:text-sm text-slate-500 mb-3">
            Hệ thống đăng ký tín chỉ tự động - An toàn & Tiện lợi
          </p>
          <div className="flex flex-wrap items-center justify-center gap-3 sm:gap-4 text-xs text-slate-400">
            <div className="flex items-center space-x-1 hover:text-blue-500 transition-colors duration-200">
              <svg className="w-4 h-4 flex-shrink-0" fill="currentColor" viewBox="0 0 20 20">
                <path
                  fillRule="evenodd"
                  d="M5 9V7a5 5 0 0110 0v2a2 2 0 012 2v5a2 2 0 01-2 2H5a2 2 0 01-2-2v-5a2 2 0 012-2zm8-2v2H7V7a3 3 0 016 0z"
                  clipRule="evenodd"
                />
              </svg>
              <span>Bảo mật cao</span>
            </div>
            <div className="flex items-center space-x-1 hover:text-green-500 transition-colors duration-200">
              <svg className="w-4 h-4 flex-shrink-0" fill="currentColor" viewBox="0 0 20 20">
                <path
                  fillRule="evenodd"
                  d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z"
                  clipRule="evenodd"
                />
              </svg>
              <span>Đã xác thực</span>
            </div>
            <div className="flex items-center space-x-1 hover:text-purple-500 transition-colors duration-200">
              <svg className="w-4 h-4 flex-shrink-0" fill="currentColor" viewBox="0 0 20 20">
                <path
                  fillRule="evenodd"
                  d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z"
                  clipRule="evenodd"
                />
              </svg>
              <span>Hỗ trợ 24/7</span>
            </div>
          </div>
        </div>
      </div>
    </FormProvider>
  );
}
