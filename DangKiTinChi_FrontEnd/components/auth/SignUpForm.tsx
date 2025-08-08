"use client";

import { useState } from "react";
import { Button } from "@/components/ui/shadcn-ui/button";
import { Input } from "@/components/ui/shadcn-ui/input";
import { Label } from "@/components/ui/shadcn-ui/label";
import { Checkbox } from "@/components/ui/shadcn-ui/checkbox";

export function SignUpForm() {
  const [formData, setFormData] = useState({
    fullName: "",
    username: "",
    password: "",
    confirmPassword: "",
    acceptTerms: false,
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (formData.password !== formData.confirmPassword) {
      alert("Mật khẩu không khớp!");
      return;
    }
    if (!formData.acceptTerms) {
      alert("Vui lòng đồng ý với các điều khoản và chính sách!");
      return;
    }
    console.log("Sign up:", formData);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  return (
    <div className="space-y-4 sm:space-y-6">
      {/* Welcome Message */}
      <div className="text-center">
        <h2 className="text-xl sm:text-2xl font-bold text-slate-800 mb-2">Tạo tài khoản mới</h2>
        <p className="text-sm sm:text-base text-slate-600">Điền thông tin để tạo tài khoản của bạn</p>
      </div>

      {/* Form */}
      <form onSubmit={handleSubmit} className="space-y-3 sm:space-y-4">
        <div className="space-y-2">
          <Label htmlFor="fullName" className="text-sm font-medium text-slate-700">
            Họ và tên
          </Label>
          <div className="relative">
            <Input
              id="fullName"
              name="fullName"
              type="text"
              value={formData.fullName}
              onChange={handleChange}
              placeholder="Nhập họ và tên đầy đủ"
              className="pl-10 h-10 sm:h-12 border-gray-300 focus:border-blue-500 focus:ring-blue-500 text-slate-700"
              required
            />
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg className="h-4 w-4 sm:h-5 sm:w-5 text-slate-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
              </svg>
            </div>
          </div>
        </div>

        <div className="space-y-2">
          <Label htmlFor="username" className="text-sm font-medium text-slate-700">
            Tên đăng nhập
          </Label>
          <div className="relative">
            <Input
              id="username"
              name="username"
              type="text"
              value={formData.username}
              onChange={handleChange}
              placeholder="Nhập tên đăng nhập"
              className="pl-10 h-10 sm:h-12 border-gray-300 focus:border-blue-500 focus:ring-blue-500 text-slate-700"
              required
            />
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg className="h-4 w-4 sm:h-5 sm:w-5 text-slate-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 8l7.89 4.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
              </svg>
            </div>
          </div>
        </div>

        <div className="space-y-2">
          <Label htmlFor="password" className="text-sm font-medium text-slate-700">
            Mật khẩu
          </Label>
          <div className="relative">
            <Input
              id="password"
              name="password"
              type="password"
              value={formData.password}
              onChange={handleChange}
              placeholder="Nhập mật khẩu"
              className="pl-10 h-10 sm:h-12 border-gray-300 focus:border-blue-500 focus:ring-blue-500 text-slate-700"
              required
            />
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg className="h-4 w-4 sm:h-5 sm:w-5 text-slate-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
              </svg>
            </div>
          </div>
        </div>

        <div className="space-y-2">
          <Label htmlFor="confirmPassword" className="text-sm font-medium text-slate-700">
            Xác nhận mật khẩu
          </Label>
          <div className="relative">
            <Input
              id="confirmPassword"
              name="confirmPassword"
              type="password"
              value={formData.confirmPassword}
              onChange={handleChange}
              placeholder="Nhập lại mật khẩu"
              className="pl-10 h-10 sm:h-12 border-gray-300 focus:border-blue-500 focus:ring-blue-500 text-slate-700"
              required
            />
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg className="h-4 w-4 sm:h-5 sm:w-5 text-slate-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
            </div>
          </div>
        </div>

        {/* Terms and Conditions */}
        <div className="flex items-start gap-3 py-2">
          <Checkbox
            id="acceptTerms"
            name="acceptTerms"
            checked={formData.acceptTerms}
            onCheckedChange={checked => handleChange({
              target: {
                name: "acceptTerms",
                type: "checkbox",
                checked: !!checked,
                value: "",
              }
            } as any)}
            className="mt-1 border-gray-300 focus:ring-blue-500 data-[state=checked]:bg-blue-600"
            aria-describedby="acceptTerms-label"
          />
          <Label
            id="acceptTerms-label"
            htmlFor="acceptTerms"
            className="text-sm text-slate-600 leading-6 cursor-pointer select-none"
          >
            Tôi đồng ý với{" "}
            <a
              href="#"
              className="text-blue-600 hover:underline font-medium transition-colors"
              tabIndex={-1}
              onClick={e => e.stopPropagation()}
            >
              Điều khoản sử dụng
            </a>{" "}
            và{" "}
            <a
              href="#"
              className="text-blue-600 hover:underline font-medium transition-colors"
              tabIndex={-1}
              onClick={e => e.stopPropagation()}
            >
              Chính sách bảo mật
            </a>
          </Label>
        </div>

        <Button
          type="submit"
          className="w-full h-10 sm:h-12 bg-gradient-to-r from-blue-600 to-sky-600 hover:from-blue-700 hover:to-sky-700 text-white font-medium transition-all duration-200 shadow-lg"
        >
          Tạo tài khoản
        </Button>
      </form>
    </div>
  );
}
