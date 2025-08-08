'use client';

import { useForm, FormProvider } from "react-hook-form";
import { Button } from "@/components/ui/shadcn-ui/button";
import { CustomInputField } from "@/components/ui/Input/CustomInputField";
import { User, Lock, UserPlus } from "lucide-react";
import { useState } from "react";

interface SignUpData {
  fullName: string;
  userName: string;
  password: string;
  confirmPassword: string;
}

export function SignUpForm() {
  const [acceptTerms, setAcceptTerms] = useState(false);
  const [termsError, setTermsError] = useState("");

  const methods = useForm<SignUpData>({
    defaultValues: {
      fullName: "",
      userName: "",
      password: "",
    },
  });

  const { handleSubmit } = methods;

  const onSubmit = (data: SignUpData) => {
    // Reset terms error
    setTermsError("");

    // Check if terms are accepted
    if (!acceptTerms) {
      setTermsError("Bạn phải chấp nhận điều khoản và chính sách để tiếp tục");
      return;
    }

    console.log("Sign up:", data);
  };

  return (
    <FormProvider {...methods}>
      <div className="w-full max-w-lg mx-auto space-y-0.5 sm:space-y-1 px-1">
        {/* Welcome Message */}
        <div className="text-center">
          <h2 className="text-sm sm:text-xl md:text-2xl font-bold text-slate-800 mb-2">
            Tạo tài khoản mới
          </h2>
          <p className="text-sm sm:text-base text-slate-600 leading-relaxed">
            Điền thông tin để tạo tài khoản của bạn
          </p>
        </div>

        {/* Form */}
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-2 sm:space-y-3">
          <div className="animate-in fade-in slide-in-from-bottom-4 duration-500 delay-100">
            <CustomInputField
              name="fullName"
              label="Họ và tên"
              placeholder="Nhập họ và tên đầy đủ"
              icon={<UserPlus className="w-4 h-4 sm:w-5 sm:h-5" />}
            />
          </div>

          <div className="animate-in fade-in slide-in-from-bottom-4 duration-500 delay-200">
            <CustomInputField
              name="username"
              label="Tên đăng nhập"
              placeholder="Nhập tên đăng nhập"
              icon={<User className="w-4 h-4 sm:w-5 sm:h-5" />}
            />
          </div>

          <div className="animate-in fade-in slide-in-from-bottom-4 duration-500 delay-300">
            <CustomInputField
              name="password"
              label="Mật khẩu"
              type="password"
              placeholder="Nhập mật khẩu"
              icon={<Lock className="w-4 h-4 sm:w-5 sm:h-5" />}
            />
          </div>

          {/* Terms and Conditions */}
          <div className="space-y-2 animate-in fade-in slide-in-from-bottom-4 duration-500 delay-400">
            <div
              className={`flex items-start space-x-3 p-4 rounded-xl border-2 transition-all duration-200 cursor-pointer hover:shadow-md ${acceptTerms
                ? 'border-blue-200 bg-blue-50/50'
                : termsError
                  ? 'border-red-200 bg-red-50/50'
                  : 'border-gray-200 bg-gray-50/50 hover:border-gray-300'
                }`}
              onClick={() => setAcceptTerms(!acceptTerms)}
            >
              <div className="flex items-center h-5 pt-0.5">
                <input
                  type="checkbox"
                  checked={acceptTerms}
                  onChange={(e) => setAcceptTerms(e.target.checked)}
                  className="w-4 h-4 sm:w-5 sm:h-5 text-blue-600 bg-white border-2 border-gray-300 rounded focus:ring-blue-500 focus:ring-1 transition-all duration-200"
                />
              </div>
              <div className="flex-1">
                <p className="text-[12px] text-gray-700 leading-relaxed">
                  Tôi đồng ý với{" "}
                  <a
                    href="#"
                    className="font-medium text-blue-600 hover:text-blue-700 underline decoration-2 underline-offset-2 transition-colors"
                    onClick={(e) => e.stopPropagation()}
                  >
                    Điều khoản sử dụng
                  </a>
                  {" "}và{" "}
                  <a
                    href="#"
                    className="font-medium text-blue-600 hover:text-blue-700 underline decoration-2 underline-offset-2 transition-colors"
                    onClick={(e) => e.stopPropagation()}
                  >
                    Chính sách bảo mật
                  </a>
                  {" "}của hệ thống
                </p>
              </div>
            </div>

            {termsError && (
              <p className="text-red-500 text-sm flex items-center space-x-2 px-1">
                <svg className="w-4 h-4 flex-shrink-0" fill="currentColor" viewBox="0 0 20 20">
                  <path fillRule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7 4a1 1 0 11-2 0 1 1 0 012 0zm-1-9a1 1 0 00-1 1v4a1 1 0 102 0V6a1 1 0 00-1-1z" clipRule="evenodd" />
                </svg>
                <span>{termsError}</span>
              </p>
            )}
          </div>

          <div className="pt-2">
            <Button
              type="submit"
              className="w-full h-11 sm:h-12 text-sm sm:text-base bg-gradient-to-r from-blue-600 to-sky-600 hover:from-blue-700 hover:to-sky-700 text-white font-semibold transition-all duration-300 shadow-lg hover:shadow-xl rounded-xl transform hover:scale-[1.02] active:scale-[0.98] animate-in fade-in slide-in-from-bottom-4 animation-delay-500"
            >
              Tạo tài khoản mới
            </Button>
          </div>
        </form>

        {/* Additional Info */}
        {/* <div className="text-center pt-6 border-t border-gray-200">
          <p className="text-sm text-slate-500 mb-4">
            Hệ thống đăng ký tín chỉ tự động - An toàn & Tiện lợi
          </p>
          <div className="flex items-center justify-center space-x-6 text-xs text-slate-400">
            <div className="flex items-center space-x-1">
              <svg className="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                <path
                  fillRule="evenodd"
                  d="M5 9V7a5 5 0 0110 0v2a2 2 0 012 2v5a2 2 0 01-2 2H5a2 2 0 01-2-2v-5a2 2 0 012-2zm8-2v2H7V7a3 3 0 016 0z"
                  clipRule="evenodd"
                />
              </svg>
              <span>Bảo mật cao</span>
            </div>
            <div className="flex items-center space-x-1">
              <svg className="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                <path
                  fillRule="evenodd"
                  d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z"
                  clipRule="evenodd"
                />
              </svg>
              <span>Đã xác thực</span>
            </div>
            <div className="flex items-center space-x-1">
              <svg className="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                <path
                  fillRule="evenodd"
                  d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z"
                  clipRule="evenodd"
                />
              </svg>
              <span>Hỗ trợ 24/7</span>
            </div>
          </div>
        </div> */}
      </div>
    </FormProvider>
  );
}
