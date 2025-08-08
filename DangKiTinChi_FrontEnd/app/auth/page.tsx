"use client";

import { useState } from "react";
import { SignInForm } from "@/components/auth/SignInForm";
import { SignUpForm } from "@/components/auth/SignUpForm";
import { AuthBackground } from "@/components/auth/AuthBackground";

export default function AuthPage() {
    const [isSignIn, setIsSignIn] = useState(true);

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 via-sky-50 to-white flex flex-col lg:flex-row">
            {/* Left side - Authentication Form */}
            <div className="w-full lg:w-1/2 flex items-center justify-center p-4 sm:p-6 lg:p-8 order-2 lg:order-1 min-h-screen lg:min-h-0">
                <div className="w-full max-w-md">
                    {/* Logo */}
                    <div className="flex items-center justify-center mb-6 lg:mb-8">
                        <div className="w-6 h-6 sm:w-8 sm:h-8 bg-gradient-to-r from-blue-600 to-sky-600 rounded-lg mr-2 sm:mr-3 flex items-center justify-center">
                            <svg className="w-3 h-3 sm:w-5 sm:h-5 text-white" fill="currentColor" viewBox="0 0 20 20">
                                <path d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                        </div>
                        <span className="text-lg sm:text-xl lg:text-2xl font-bold bg-gradient-to-r from-blue-700 to-sky-700 bg-clip-text text-transparent">
                            Đăng ký tín chỉ tự động
                        </span>
                    </div>

                    {/* Toggle Buttons */}
                    <div className="flex bg-gray-50 rounded-full p-1 mb-6 lg:mb-8 border border-gray-200">
                        <button
                            onClick={() => setIsSignIn(true)}
                            className={`flex-1 py-2 px-4 rounded-full text-sm font-medium transition-all duration-200 ${isSignIn
                                ? "bg-white text-blue-700 shadow-sm border border-blue-200"
                                : "text-gray-600 hover:text-blue-600"
                                }`}
                        >
                            Đăng nhập
                        </button>
                        <button
                            onClick={() => setIsSignIn(false)}
                            className={`flex-1 py-2 px-4 rounded-full text-sm font-medium transition-all duration-200 ${!isSignIn
                                ? "bg-white text-blue-700 shadow-sm border border-blue-200"
                                : "text-gray-600 hover:text-blue-600"
                                }`}
                        >
                            Đăng ký
                        </button>
                    </div>

                    {/* Forms */}
                    {isSignIn ? <SignInForm /> : <SignUpForm />}
                </div>
            </div>

            {/* Right side - Background & Info */}
            <div className="w-full lg:w-1/2 relative order-1 lg:order-2 min-h-[300px] lg:min-h-screen lg:block hidden">
                <AuthBackground />
            </div>
        </div>
    );
}
