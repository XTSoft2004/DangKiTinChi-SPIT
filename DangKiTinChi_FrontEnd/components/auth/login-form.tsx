"use client";

import { useState } from "react";
import { Button } from "@/components/ui/shadcn-ui/button";
import { Input } from "@/components/ui/shadcn-ui/input";
import { Label } from "@/components/ui/shadcn-ui/label";
import { Card } from "@/components/ui/shadcn-ui/card";
import { Eye, EyeOff, Mail, Lock } from "lucide-react";
import { toast } from "sonner";
import { getOrCreateDeviceId } from "@/utils/deviceId";
import { ILogin } from "@/types/auth";
import { login } from "@/actions/auth.actions";
import { useRouter } from "next/navigation";

interface LoginFormProps {
  onSwitchToRegister: () => void;
}

export function LoginForm({ onSwitchToRegister }: LoginFormProps) {
  const router = useRouter();
  const [showPassword, setShowPassword] = useState(false);
  const deviceId = getOrCreateDeviceId();
  const [formData, setFormData] = useState({
    userName: "",
    password: "",
    deviceId: deviceId,
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const loginData: ILogin = {
      userName: formData.userName,
      password: formData.password,
      deviceId: formData.deviceId,
    };

    try {
      const response = await login(loginData);
      if (response.ok) {
        toast.success("Đăng nhập thành công!", {
          description: "Chào mừng bạn trở lại!",
        });
        router.push("/");
      } else {
        toast.error("Đăng nhập thất bại", {
          description: "Vui lòng kiểm tra lại thông tin đăng nhập.",
        });
      }
    } catch (error: any) {
      toast.error("Đã xảy ra lỗi khi đăng nhập", {
        description: error?.message || "Vui lòng thử lại sau.",
      });
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  return (
    <div className="w-full max-w-md mx-auto">
      <div className="text-center mb-8">
        <h2 className="text-3xl font-bold text-slate-800 mb-2">
          Chào mừng trở lại!
        </h2>
        <p className="text-slate-600">Đăng nhập vào tài khoản của bạn</p>
      </div>

      <Card className="p-8 border border-cyan-100/50 bg-white/90 backdrop-blur shadow-lg">
        <form onSubmit={handleSubmit} className="space-y-6">
          <div className="space-y-2">
            <Label htmlFor="userName" className="text-slate-700 font-medium">
              Tên đăng nhập
            </Label>
            <div className="relative">
              <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-slate-400" />
              <Input
                id="userName"
                name="userName"
                type="text"
                placeholder="Nhập tên đăng nhập"
                value={formData.userName}
                onChange={handleChange}
                className="pl-10 h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400"
                required
              />
            </div>
          </div>

          <div className="space-y-2">
            <Label htmlFor="password" className="text-slate-700 font-medium">
              Mật khẩu
            </Label>
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-slate-400" />
              <Input
                id="password"
                name="password"
                type={showPassword ? "text" : "password"}
                placeholder="Nhập mật khẩu của bạn"
                value={formData.password}
                onChange={handleChange}
                className="pl-10 pr-10 h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400"
                required
              />
              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute right-3 top-1/2 transform -translate-y-1/2 text-slate-400 hover:text-slate-600 transition-colors"
              >
                {showPassword ? (
                  <EyeOff className="h-4 w-4" />
                ) : (
                  <Eye className="h-4 w-4" />
                )}
              </button>
            </div>
          </div>

          <div className="flex items-center justify-between">
            <label className="flex items-center space-x-2 cursor-pointer">
              <input
                type="checkbox"
                className="w-4 h-4 text-cyan-600 border-cyan-300 rounded focus:ring-cyan-500"
              />
              <span className="text-sm text-slate-600">Ghi nhớ đăng nhập</span>
            </label>
            <button
              type="button"
              className="text-sm text-cyan-600 hover:text-cyan-700 transition-colors"
            >
              Quên mật khẩu?
            </button>
          </div>

          <Button
            type="submit"
            className="w-full h-12 bg-gradient-to-r from-cyan-500 to-cyan-600 hover:from-cyan-600 hover:to-cyan-700 text-white font-medium text-base shadow-lg shadow-cyan-500/25 transition-all duration-200"
          >
            Đăng nhập
          </Button>

          <div className="relative">
            <div className="absolute inset-0 flex items-center">
              <div className="w-full border-t border-slate-200"></div>
            </div>
            <div className="relative flex justify-center text-sm">
              <span className="px-4 bg-white text-slate-500">hoặc</span>
            </div>
          </div>

          <div className="text-center">
            <span className="text-slate-600">Chưa có tài khoản? </span>
            <button
              type="button"
              onClick={onSwitchToRegister}
              className="text-cyan-600 hover:text-cyan-700 font-medium transition-colors"
            >
              Đăng ký ngay
            </button>
          </div>
        </form>
      </Card>
    </div>
  );
}
