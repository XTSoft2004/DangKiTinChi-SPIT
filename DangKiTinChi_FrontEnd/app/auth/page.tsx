"use client"

import { useState } from "react"
import { LoginForm } from "@/components/auth/login-form"
import { RegisterForm } from "@/components/auth/register-form"
import { AuthContent } from "@/components/auth/auth-content"
import { AuthToggle } from "@/components/auth/auth-toggle"

export default function AuthPage() {
  const [isLogin, setIsLogin] = useState(true)

  const switchToRegister = () => setIsLogin(false)
  const switchToLogin = () => setIsLogin(true)
  const toggleAuth = () => setIsLogin(!isLogin)

  return (
    <div className="h-screen flex flex-col lg:flex-row relative overflow-hidden">
      {/* Floating Toggle */}
      <AuthToggle isLogin={isLogin} onToggle={toggleAuth} />
      
      {/* Mobile Header */}
      <div className="lg:hidden bg-gradient-to-r from-cyan-600 to-cyan-700 p-6 text-center">
        <div className="flex items-center justify-center space-x-3 mb-2">
          <div className="p-2 rounded-xl bg-white/20">
            <svg className="h-6 w-6 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 14l9-5-9-5-9 5 9 5z" />
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 14l6.16-3.422a12.083 12.083 0 01.665 6.479A11.952 11.952 0 0012 20.055a11.952 11.952 0 00-6.824-2.998 12.078 12.078 0 01.665-6.479L12 14z" />
            </svg>
          </div>
          <span className="text-xl font-bold text-white">SPIT System</span>
        </div>
        <p className="text-cyan-100 text-sm">
          {isLogin ? "Đăng nhập vào tài khoản của bạn" : "Tạo tài khoản mới để bắt đầu"}
        </p>
      </div>

      {isLogin ? (
        <>
          {/* Login Form */}
          <div className="flex-1 flex items-center justify-center p-4 lg:p-8 bg-gradient-to-br from-slate-50 to-cyan-50">
            <LoginForm onSwitchToRegister={switchToRegister} />
          </div>
          {/* Content - Hidden on mobile, shown on desktop */}
          <div className="hidden lg:flex flex-1 bg-gradient-to-br from-cyan-600 via-cyan-700 to-teal-700 relative overflow-hidden">
            {/* Background decorative elements */}
            <div className="absolute top-0 right-0 w-96 h-96 bg-white/10 rounded-full -translate-y-48 translate-x-48"></div>
            <div className="absolute bottom-0 left-0 w-72 h-72 bg-white/5 rounded-full translate-y-36 -translate-x-36"></div>
            <div className="absolute top-1/2 right-1/4 w-32 h-32 bg-white/10 rounded-full"></div>
            
            <AuthContent isLogin={isLogin} />
          </div>
        </>
      ) : (
        <>
          {/* Content - Hidden on mobile, shown on desktop */}
          <div className="hidden lg:flex flex-1 bg-gradient-to-br from-cyan-600 via-cyan-700 to-teal-700 relative overflow-hidden">
            {/* Background decorative elements */}
            <div className="absolute top-0 left-0 w-96 h-96 bg-white/10 rounded-full -translate-y-48 -translate-x-48"></div>
            <div className="absolute bottom-0 right-0 w-72 h-72 bg-white/5 rounded-full translate-y-36 translate-x-36"></div>
            <div className="absolute top-1/3 left-1/4 w-24 h-24 bg-white/10 rounded-full"></div>
            
            <AuthContent isLogin={isLogin} />
          </div>
          {/* Register Form */}
          <div className="flex-1 flex items-center justify-center p-4 lg:p-8 bg-gradient-to-br from-slate-50 to-cyan-50">
            <RegisterForm onSwitchToLogin={switchToLogin} />
          </div>
        </>
      )}
    </div>
  )
}
