"use client"

import { useState } from "react"
import { Button } from "@/components/ui/shadcn-ui/button"
import { Input } from "@/components/ui/shadcn-ui/input"
import { Label } from "@/components/ui/shadcn-ui/label"
import { Card } from "@/components/ui/shadcn-ui/card"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/shadcn-ui/select"
import { Eye, EyeOff, Mail, Lock, User, Calendar } from "lucide-react"

interface RegisterFormProps {
  onSwitchToLogin: () => void
}

export function RegisterForm({ onSwitchToLogin }: RegisterFormProps) {
  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)
  const [formData, setFormData] = useState({
    fullName: "",
    email: "",
    studentId: "",
    major: "",
    year: "",
    password: "",
    confirmPassword: ""
  })

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    // Handle register logic here
    console.log("Register:", formData)
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData(prev => ({
      ...prev,
      [e.target.name]: e.target.value
    }))
  }

  return (
    <div className="w-full max-w-md mx-auto">
      <div className="text-center mb-8">
        <h2 className="text-3xl font-bold text-slate-800 mb-2">Tạo tài khoản mới</h2>
        <p className="text-slate-600">Đăng ký để sử dụng hệ thống SPIT</p>
      </div>

      <Card className="p-8 border border-cyan-100/50 bg-white/90 backdrop-blur shadow-lg">
        <form onSubmit={handleSubmit} className="space-y-6">
          {/* Full Name */}
          <div className="space-y-2">
            <Label htmlFor="fullName" className="text-slate-700 font-medium">
              Họ và tên
            </Label>
            <div className="relative">
              <User className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-slate-400" />
              <Input
                id="fullName"
                name="fullName"
                type="text"
                placeholder="Nhập họ và tên đầy đủ"
                value={formData.fullName}
                onChange={handleChange}
                className="pl-10 h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400"
                required
              />
            </div>
          </div>

          {/* Email */}
          <div className="space-y-2">
            <Label htmlFor="email" className="text-slate-700 font-medium">
              Email sinh viên
            </Label>
            <div className="relative">
              <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-slate-400" />
              <Input
                id="email"
                name="email"
                type="email"
                placeholder="your.email@university.edu"
                value={formData.email}
                onChange={handleChange}
                className="pl-10 h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400"
                required
              />
            </div>
          </div>

          {/* Student ID & Major */}
          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
              <Label htmlFor="studentId" className="text-slate-700 font-medium">
                Mã sinh viên
              </Label>
              <Input
                id="studentId"
                name="studentId"
                type="text"
                placeholder="20xxxxxx"
                value={formData.studentId}
                onChange={handleChange}
                className="h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400"
                required
              />
            </div>
            <div className="space-y-2">
              <Label htmlFor="year" className="text-slate-700 font-medium">
                Năm học
              </Label>
              <Select>
                <SelectTrigger className="h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400">
                  <SelectValue placeholder="Chọn năm" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="2024">Năm 1 (2024)</SelectItem>
                  <SelectItem value="2023">Năm 2 (2023)</SelectItem>
                  <SelectItem value="2022">Năm 3 (2022)</SelectItem>
                  <SelectItem value="2021">Năm 4 (2021)</SelectItem>
                </SelectContent>
              </Select>
            </div>
          </div>

          {/* Major */}
          <div className="space-y-2">
            <Label htmlFor="major" className="text-slate-700 font-medium">
              Chuyên ngành
            </Label>
            <Select>
              <SelectTrigger className="h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400">
                <SelectValue placeholder="Chọn chuyên ngành" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="computer-science">Khoa học máy tính</SelectItem>
                <SelectItem value="software-engineering">Kỹ thuật phần mềm</SelectItem>
                <SelectItem value="information-technology">Công nghệ thông tin</SelectItem>
                <SelectItem value="data-science">Khoa học dữ liệu</SelectItem>
                <SelectItem value="cybersecurity">An toàn thông tin</SelectItem>
              </SelectContent>
            </Select>
          </div>

          {/* Password */}
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
                placeholder="Tạo mật khẩu mạnh"
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

          {/* Confirm Password */}
          <div className="space-y-2">
            <Label htmlFor="confirmPassword" className="text-slate-700 font-medium">
              Xác nhận mật khẩu
            </Label>
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-slate-400" />
              <Input
                id="confirmPassword"
                name="confirmPassword"
                type={showConfirmPassword ? "text" : "password"}
                placeholder="Nhập lại mật khẩu"
                value={formData.confirmPassword}
                onChange={handleChange}
                className="pl-10 pr-10 h-12 border-cyan-200 focus:border-cyan-400 focus:ring-cyan-400"
                required
              />
              <button
                type="button"
                onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                className="absolute right-3 top-1/2 transform -translate-y-1/2 text-slate-400 hover:text-slate-600 transition-colors"
              >
                {showConfirmPassword ? (
                  <EyeOff className="h-4 w-4" />
                ) : (
                  <Eye className="h-4 w-4" />
                )}
              </button>
            </div>
          </div>

          {/* Terms & Conditions */}
          <div className="flex items-start space-x-2">
            <input
              type="checkbox"
              id="terms"
              className="mt-1 w-4 h-4 text-cyan-600 border-cyan-300 rounded focus:ring-cyan-500"
              required
            />
            <Label htmlFor="terms" className="text-sm text-slate-600 leading-relaxed">
              Tôi đồng ý với{" "}
              <button type="button" className="text-cyan-600 hover:text-cyan-700 transition-colors">
                Điều khoản dịch vụ
              </button>{" "}
              và{" "}
              <button type="button" className="text-cyan-600 hover:text-cyan-700 transition-colors">
                Chính sách bảo mật
              </button>
            </Label>
          </div>

          {/* Register Button */}
          <Button
            type="submit"
            className="w-full h-12 bg-gradient-to-r from-cyan-500 to-cyan-600 hover:from-cyan-600 hover:to-cyan-700 text-white font-medium text-base shadow-lg shadow-cyan-500/25 transition-all duration-200"
          >
            Tạo tài khoản
          </Button>

          {/* Divider */}
          <div className="relative">
            <div className="absolute inset-0 flex items-center">
              <div className="w-full border-t border-slate-200"></div>
            </div>
            <div className="relative flex justify-center text-sm">
              <span className="px-4 bg-white text-slate-500">hoặc</span>
            </div>
          </div>

          {/* Switch to Login */}
          <div className="text-center">
            <span className="text-slate-600">Đã có tài khoản? </span>
            <button
              type="button"
              onClick={onSwitchToLogin}
              className="text-cyan-600 hover:text-cyan-700 font-medium transition-colors"
            >
              Đăng nhập ngay
            </button>
          </div>
        </form>
      </Card>
    </div>
  )
}
