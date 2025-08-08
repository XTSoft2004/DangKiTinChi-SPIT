"use client";

const FeatureItem = ({ text }: { text: string }) => (
    <div className="flex items-center space-x-1.5 sm:space-x-2">
        <div className="w-3 h-3 sm:w-4 sm:h-4 md:w-5 md:h-5 bg-blue-400 rounded-full flex items-center justify-center flex-shrink-0">
            <svg className="w-2 h-2 sm:w-2.5 sm:h-2.5 md:w-3 md:h-3 text-white" fill="currentColor" viewBox="0 0 20 20">
                <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
            </svg>
        </div>
        <span className="text-blue-100 text-xs sm:text-sm">{text}</span>
    </div>
);

const StatItem = ({ value, label }: { value: string; label: string }) => (
    <div>
        <div className="text-sm sm:text-base md:text-lg font-bold text-white">{value}</div>
        <div className="text-xs text-blue-200">{label}</div>
    </div>
);

const FloatingDot = ({
    position,
    size,
    color,
    animationDelay = 0
}: {
    position: string;
    size: string;
    color: string;
    animationDelay?: number;
}) => (
    <div
        className={`absolute ${position} ${size} ${color} rounded-full animate-pulse`}
        style={{ animationDelay: `${animationDelay}ms` }}
    ></div>
);

const BackgroundCircle = ({ position, size }: { position: string; size: string }) => (
    <div className={`absolute ${position} ${size} bg-white rounded-full`}></div>
);

export function AuthBackground() {
    return (
        <div className="h-full bg-gradient-to-br from-blue-400 via-sky-500 to-indigo-600 relative overflow-hidden flex flex-col justify-center items-center p-2 sm:p-3 md:p-4 lg:p-6 xl:p-8">
            {/* Background Pattern */}
            <div className="absolute inset-0 opacity-10">
                <BackgroundCircle position="top-6 sm:top-8 md:top-12 left-3 sm:left-4 md:left-6" size="w-6 h-6 sm:w-8 sm:h-8 md:w-12 md:h-12" />
                <BackgroundCircle position="top-12 sm:top-16 md:top-24 right-6 sm:right-8 md:right-12" size="w-4 h-4 sm:w-6 sm:h-6 md:w-10 md:h-10" />
                <BackgroundCircle position="bottom-10 sm:bottom-12 md:bottom-20 left-4 sm:left-6 md:left-10" size="w-3 h-3 sm:w-5 sm:h-5 md:w-8 md:h-8" />
                <BackgroundCircle position="bottom-6 sm:bottom-8 md:bottom-12 right-3 sm:right-4 md:right-6" size="w-8 h-8 sm:w-12 sm:h-12 md:w-16 md:h-16" />

                {/* Grid Pattern */}
                <div className="absolute inset-0 bg-grid-pattern opacity-20"></div>
            </div>

            {/* Content */}
            <div className="relative z-10 text-center text-white max-w-xs sm:max-w-sm md:max-w-md lg:max-w-lg">
                {/* 3D Illustration */}
                <div className="mb-2 sm:mb-3 md:mb-4 lg:mb-6 flex justify-center">
                    <div className="relative">
                        {/* Safe/Vault Illustration */}
                        <div className="w-16 h-16 sm:w-20 sm:h-20 md:w-28 md:h-28 lg:w-36 lg:h-36 bg-gradient-to-br from-blue-400 to-sky-600 rounded-lg sm:rounded-xl md:rounded-2xl shadow-2xl transform rotate-6 hover:rotate-3 transition-transform duration-300">
                            {/* Safe Door */}
                            <div className="absolute inset-1 sm:inset-1.5 md:inset-2 bg-gradient-to-br from-sky-500 to-indigo-700 rounded-md sm:rounded-lg md:rounded-xl">
                                {/* Handle */}
                                <div className="absolute top-1/2 right-1 sm:right-1.5 md:right-2 w-3 h-0.5 sm:w-4 sm:h-1 md:w-6 md:h-1.5 bg-gradient-to-r from-gray-300 to-gray-400 rounded-full transform -translate-y-1/2"></div>

                                {/* Lock Dial */}
                                <div className="absolute top-1/2 left-1/2 w-7 h-7 sm:w-10 sm:h-10 md:w-12 md:h-12 lg:w-16 lg:h-16 bg-gradient-to-br from-blue-400 to-blue-600 rounded-full transform -translate-x-1/2 -translate-y-1/2 flex items-center justify-center">
                                    <div className="w-5 h-5 sm:w-7 sm:h-7 md:w-9 md:h-9 lg:w-12 lg:h-12 bg-gradient-to-br from-blue-500 to-blue-700 rounded-full flex items-center justify-center">
                                        <div className="w-3 h-3 sm:w-4 sm:h-4 md:w-6 md:h-6 lg:w-7 lg:h-7 bg-white rounded-full flex items-center justify-center">
                                            <div className="w-0.5 h-2 sm:w-0.5 sm:h-3 md:w-1 md:h-4 lg:w-1 lg:h-5 bg-blue-600 rounded-full"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            {/* Shadow */}
                            <div className="absolute -bottom-1 -right-1 sm:-bottom-1.5 sm:-right-1.5 md:-bottom-2 md:-right-2 w-16 h-16 sm:w-20 sm:h-20 md:w-28 md:h-28 lg:w-36 lg:h-36 bg-black opacity-20 rounded-lg sm:rounded-xl md:rounded-2xl transform rotate-6 -z-10"></div>
                        </div>
                    </div>
                </div>

                {/* Title */}
                <h1 className="text-sm sm:text-base md:text-lg lg:text-xl font-bold mb-1 sm:mb-2 lg:mb-3 leading-tight">
                    Hệ thống đăng ký tín chỉ
                    <br />
                    <span className="text-blue-200">an toàn & tiện lợi</span>
                </h1>

                {/* Description */}
                <p className="text-blue-100 text-xs sm:text-sm md:text-base mb-2 sm:mb-3 md:mb-4 lg:mb-6 leading-relaxed px-1 sm:px-2 md:px-0">
                    Tham gia cùng hàng triệu sinh viên thông minh đã tin tưởng chúng tôi để quản lý việc đăng ký học phần.
                    <span className="hidden lg:inline"> Đăng nhập để truy cập bảng điều khiển cá nhân, theo dõi tiến độ học tập và đưa ra quyết định học tập sáng suốt.</span>
                </p>

                {/* Features */}
                <div className="space-y-1.5 sm:space-y-2 md:space-y-3 text-left">
                    <FeatureItem text="Đăng ký học phần nhanh chóng" />
                    <FeatureItem text="Theo dõi tiến độ học tập" />
                    <FeatureItem text="Bảo mật thông tin cao" />
                    <FeatureItem text="Hỗ trợ 24/7" />
                </div>

                {/* Stats */}
                <div className="mt-3 sm:mt-4 md:mt-6 lg:mt-8 grid grid-cols-2 gap-2 sm:gap-3 md:gap-4 text-center">
                    <StatItem value="50K+" label="Sinh viên tin tưởng" />
                    <StatItem value="99.9%" label="Thời gian hoạt động" />
                </div>
            </div>

            {/* Floating Elements */}
            <FloatingDot
                position="top-3 sm:top-4 md:top-6 right-3 sm:right-4 md:right-6"
                size="w-1 h-1 sm:w-1.5 sm:h-1.5 md:w-2 md:h-2"
                color="bg-yellow-300"
            />
            <FloatingDot
                position="bottom-3 sm:bottom-4 md:bottom-6 left-3 sm:left-4 md:left-6"
                size="w-1 h-1 sm:w-1.5 sm:h-1.5"
                color="bg-blue-300"
                animationDelay={1000}
            />
            <FloatingDot
                position="top-1/3 left-2 sm:left-3 md:left-4"
                size="w-1.5 h-1.5 sm:w-2 sm:h-2 md:w-3 md:h-3"
                color="bg-sky-300"
                animationDelay={500}
            />
        </div>
    );
}
