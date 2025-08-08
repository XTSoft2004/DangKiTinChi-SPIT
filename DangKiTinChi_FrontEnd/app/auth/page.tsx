"use client";

import { useState } from "react";
import { SignInForm } from "@/components/auth/SignInForm";
import { SignUpForm } from "@/components/auth/SignUpForm";
import { AuthBackground } from "@/components/auth/AuthBackground";

export default function AuthPage() {
    const [isSignIn, setIsSignIn] = useState(true);

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-50 via-sky-50 to-white flex flex-col lg:flex-row overflow-hidden">
            {/* Left side - Authentication Form */}
            <div className="w-full lg:w-1/2 flex items-center justify-center p-4 sm:p-6 md:p-8 lg:p-8 xl:p-12 order-2 lg:order-1 min-h-screen lg:min-h-0">
                <div className="w-full max-w-sm sm:max-w-lg animate-in fade-in duration-700 slide-in-from-bottom-4">
                    {/* Logo */}
                    <div className="flex items-center justify-center mb-2 sm:mb-4 animate-in fade-in duration-500 delay-150">
                        <div className="w-8 h-8 sm:w-10 sm:h-10 bg-gradient-to-r from-blue-600 to-sky-600 rounded-lg mr-3 flex items-center justify-center transform hover:scale-110 transition-transform duration-200">
                            <svg className="w-4 h-4 sm:w-5 sm:h-5 text-white" fill="currentColor" viewBox="0 0 20 20">
                                <path d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                            </svg>
                        </div>
                        <span className="text-lg sm:text-xl md:text-2xl font-bold bg-gradient-to-r from-blue-700 to-sky-700 bg-clip-text text-transparent">
                            Đăng ký tín chỉ tự động
                        </span>
                    </div>

                    {/* Toggle Buttons */}
                    <div className="relative flex bg-gray-100 rounded-2xl p-1 mb-2 sm:mb-3 border border-gray-200 overflow-hidden animate-in fade-in duration-500 delay-300">
                        {/* Sliding background indicator */}
                        <div
                            className={`absolute top-1 bottom-1 w-1/2 bg-white rounded-xl shadow-md border border-blue-200 transition-transform duration-300 ease-in-out transform ${isSignIn ? 'translate-x-0' : 'translate-x-full'} ${isSignIn ? '' : 'left-[-5px]'}`}
                        />

                        <button
                            onClick={() => setIsSignIn(true)}
                            className={`relative z-10 flex-1 py-3 px-4 rounded-xl text-sm sm:text-base font-semibold transition-all duration-300 transform hover:scale-105 active:scale-95 ${isSignIn
                                ? "text-blue-700"
                                : "text-gray-600 hover:text-blue-600"
                                }`}
                        >
                            Đăng nhập
                        </button>
                        <button
                            onClick={() => setIsSignIn(false)}
                            className={`relative z-10 flex-1 py-3 px-4 rounded-xl text-sm sm:text-base font-semibold transition-all duration-300 transform hover:scale-105 active:scale-95 ${!isSignIn
                                ? "text-blue-700"
                                : "text-gray-600 hover:text-blue-600"
                                }`}
                        >
                            Đăng ký
                        </button>
                    </div>

                    {/* Forms */}
                    <div className="relative animate-in fade-in duration-500 delay-500">
                        <div
                            className={`transition-all duration-500 ease-in-out ${isSignIn
                                ? 'opacity-100 scale-100 translate-y-0'
                                : 'opacity-0 scale-95 -translate-y-4 absolute top-0 left-0 w-full pointer-events-none'
                                }`}
                        >
                            <SignInForm />
                        </div>
                        <div
                            className={`transition-all duration-500 ease-in-out ${!isSignIn
                                ? 'opacity-100 scale-100 translate-y-0'
                                : 'opacity-0 scale-95 translate-y-4 absolute top-0 left-0 w-full pointer-events-none'
                                }`}
                        >
                            <SignUpForm />
                        </div>
                    </div>
                </div>
            </div>

            {/* Right side - Background & Info */}
            <div className="hidden lg:block w-full lg:w-1/2 relative order-1 lg:order-2 min-h-screen">
                <div className="h-full animate-in slide-in-from-right duration-700">
                    <AuthBackground />
                </div>
            </div>
        </div>
    );
}
