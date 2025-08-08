"use client";

export function AuthBackground() {
    return (
        <div className="h-full bg-gradient-to-br from-blue-400 via-sky-500 to-indigo-600 relative overflow-hidden flex flex-col justify-center items-center p-4 sm:p-6 lg:p-12">
            {/* Background Pattern */}
            <div className="absolute inset-0 opacity-10">
                <div className="absolute top-10 sm:top-20 left-5 sm:left-10 w-12 h-12 sm:w-20 sm:h-20 bg-white rounded-full"></div>
                <div className="absolute top-20 sm:top-40 right-10 sm:right-20 w-10 h-10 sm:w-16 sm:h-16 bg-white rounded-full"></div>
                <div className="absolute bottom-16 sm:bottom-32 left-8 sm:left-16 w-8 h-8 sm:w-12 sm:h-12 bg-white rounded-full"></div>
                <div className="absolute bottom-10 sm:bottom-20 right-5 sm:right-10 w-16 h-16 sm:w-24 sm:h-24 bg-white rounded-full"></div>

                {/* Grid Pattern */}
                <div className="absolute inset-0 bg-grid-pattern opacity-20"></div>
            </div>

            {/* Content */}
            <div className="relative z-10 text-center text-white max-w-xs sm:max-w-md lg:max-w-lg">
                {/* 3D Illustration */}
                <div className="mb-4 sm:mb-6 lg:mb-8 flex justify-center">
                    <div className="relative">
                        {/* Safe/Vault Illustration */}
                        <div className="w-32 h-32 sm:w-40 sm:h-40 lg:w-48 lg:h-48 bg-gradient-to-br from-blue-400 to-sky-600 rounded-2xl sm:rounded-3xl shadow-2xl relative transform rotate-6 hover:rotate-3 transition-transform duration-300">
                            {/* Safe Door */}
                            <div className="absolute inset-2 sm:inset-4 bg-gradient-to-br from-sky-500 to-indigo-700 rounded-xl sm:rounded-2xl">
                                {/* Handle */}
                                <div className="absolute top-1/2 right-2 sm:right-4 w-6 h-1.5 sm:w-8 sm:h-2 bg-gradient-to-r from-gray-300 to-gray-400 rounded-full transform -translate-y-1/2"></div>

                                {/* Lock Dial */}
                                <div className="absolute top-1/2 left-1/2 w-14 h-14 sm:w-16 sm:h-16 lg:w-20 lg:h-20 bg-gradient-to-br from-blue-400 to-blue-600 rounded-full transform -translate-x-1/2 -translate-y-1/2 flex items-center justify-center">
                                    <div className="w-10 h-10 sm:w-12 sm:h-12 lg:w-14 lg:h-14 bg-gradient-to-br from-blue-500 to-blue-700 rounded-full flex items-center justify-center">
                                        <div className="w-6 h-6 sm:w-7 sm:h-7 lg:w-8 lg:h-8 bg-white rounded-full flex items-center justify-center">
                                            <div className="w-0.5 h-4 sm:w-1 sm:h-5 lg:w-1 lg:h-6 bg-blue-600 rounded-full"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            {/* Shadow */}
                            <div className="absolute -bottom-2 -right-2 sm:-bottom-4 sm:-right-4 w-32 h-32 sm:w-40 sm:h-40 lg:w-48 lg:h-48 bg-black opacity-20 rounded-2xl sm:rounded-3xl transform rotate-6 -z-10"></div>
                        </div>
                    </div>
                </div>

                {/* Title */}
                <h1 className="text-xl sm:text-2xl lg:text-3xl font-bold mb-2 sm:mb-3 lg:mb-4 leading-tight">
                    Hệ thống đăng ký tín chỉ
                    <br />
                    <span className="text-blue-200">an toàn & tiện lợi</span>
                </h1>

                {/* Description */}
                <p className="text-blue-100 text-sm sm:text-base lg:text-lg mb-4 sm:mb-6 lg:mb-8 leading-relaxed px-2 sm:px-0">
                    Tham gia cùng hàng triệu sinh viên thông minh đã tin tưởng chúng tôi để quản lý việc đăng ký học phần.
                    <span className="hidden sm:inline"> Đăng nhập để truy cập bảng điều khiển cá nhân, theo dõi tiến độ học tập và đưa ra quyết định học tập sáng suốt.</span>
                </p>

                {/* Features */}
                <div className="space-y-3 sm:space-y-4 text-left">
                    <div className="flex items-center space-x-3">
                        <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-400 rounded-full flex items-center justify-center flex-shrink-0">
                            <svg className="w-3 h-3 sm:w-4 sm:h-4 text-white" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
                            </svg>
                        </div>
                        <span className="text-blue-100 text-sm sm:text-base">Đăng ký học phần nhanh chóng</span>
                    </div>

                    <div className="flex items-center space-x-3">
                        <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-400 rounded-full flex items-center justify-center flex-shrink-0">
                            <svg className="w-3 h-3 sm:w-4 sm:h-4 text-white" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
                            </svg>
                        </div>
                        <span className="text-blue-100 text-sm sm:text-base">Theo dõi tiến độ học tập</span>
                    </div>

                    <div className="flex items-center space-x-3">
                        <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-400 rounded-full flex items-center justify-center flex-shrink-0">
                            <svg className="w-3 h-3 sm:w-4 sm:h-4 text-white" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
                            </svg>
                        </div>
                        <span className="text-blue-100 text-sm sm:text-base">Bảo mật thông tin cao</span>
                    </div>

                    <div className="flex items-center space-x-3">
                        <div className="w-5 h-5 sm:w-6 sm:h-6 bg-blue-400 rounded-full flex items-center justify-center flex-shrink-0">
                            <svg className="w-3 h-3 sm:w-4 sm:h-4 text-white" fill="currentColor" viewBox="0 0 20 20">
                                <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
                            </svg>
                        </div>
                        <span className="text-blue-100 text-sm sm:text-base">Hỗ trợ 24/7</span>
                    </div>
                </div>

                {/* Stats */}
                <div className="mt-6 sm:mt-8 lg:mt-12 grid grid-cols-2 gap-4 sm:gap-6 text-center">
                    <div>
                        <div className="text-xl sm:text-2xl font-bold text-white">50K+</div>
                        <div className="text-xs sm:text-sm text-blue-200">Sinh viên tin tưởng</div>
                    </div>
                    <div>
                        <div className="text-xl sm:text-2xl font-bold text-white">99.9%</div>
                        <div className="text-xs sm:text-sm text-blue-200">Thời gian hoạt động</div>
                    </div>
                </div>
            </div>

            {/* Floating Elements */}
            <div className="absolute top-5 sm:top-10 right-5 sm:right-10 w-2 h-2 sm:w-3 sm:h-3 bg-yellow-300 rounded-full animate-pulse"></div>
            <div className="absolute bottom-5 sm:bottom-10 left-5 sm:left-10 w-1.5 h-1.5 sm:w-2 sm:h-2 bg-blue-300 rounded-full animate-pulse delay-1000"></div>
            <div className="absolute top-1/3 left-4 sm:left-8 w-3 h-3 sm:w-4 sm:h-4 bg-sky-300 rounded-full animate-pulse delay-500"></div>
        </div>
    );
}
