"use client";

const FeatureItem = ({ text }: { text: string }) => (
    <div className="flex items-center space-x-2">
        <div className="w-4 h-4 bg-blue-400 rounded-full flex items-center justify-center flex-shrink-0">
            <svg className="w-2.5 h-2.5 text-white" fill="currentColor" viewBox="0 0 20 20">
                <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
            </svg>
        </div>
        <span className="text-blue-100 text-sm">{text}</span>
    </div>
);

const StatItem = ({ value, label }: { value: string; label: string }) => (
    <div>
        <div className="text-lg font-bold text-white">{value}</div>
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
        <div className="h-full bg-gradient-to-br from-blue-400 via-sky-500 to-indigo-600 relative overflow-hidden flex flex-col justify-center items-center p-6 lg:p-8">
            {/* Background Pattern */}
            <div className="absolute inset-0 opacity-10">
                <BackgroundCircle position="top-12 left-6" size="w-12 h-12" />
                <BackgroundCircle position="top-24 right-12" size="w-10 h-10" />
                <BackgroundCircle position="bottom-20 left-10" size="w-8 h-8" />
                <BackgroundCircle position="bottom-12 right-6" size="w-16 h-16" />

                {/* Grid Pattern */}
                <div className="absolute inset-0 bg-grid-pattern opacity-20"></div>
            </div>

            {/* Content */}
            <div className="relative z-10 text-center text-white max-w-md">
                {/* 3D Illustration */}
                <div className="mb-6 flex justify-center">
                    <div className="relative">
                        {/* Safe/Vault Illustration */}
                        <div className="w-32 h-32 bg-gradient-to-br from-blue-400 to-sky-600 rounded-2xl shadow-2xl transform rotate-6 hover:rotate-3 transition-transform duration-300">
                            {/* Safe Door */}
                            <div className="absolute inset-2 bg-gradient-to-br from-sky-500 to-indigo-700 rounded-xl">
                                {/* Handle */}
                                <div className="absolute top-1/2 right-2 w-6 h-1.5 bg-gradient-to-r from-gray-300 to-gray-400 rounded-full transform -translate-y-1/2"></div>

                                {/* Lock Dial */}
                                <div className="absolute top-1/2 left-1/2 w-14 h-14 bg-gradient-to-br from-blue-400 to-blue-600 rounded-full transform -translate-x-1/2 -translate-y-1/2 flex items-center justify-center">
                                    <div className="w-10 h-10 bg-gradient-to-br from-blue-500 to-blue-700 rounded-full flex items-center justify-center">
                                        <div className="w-6 h-6 bg-white rounded-full flex items-center justify-center">
                                            <div className="w-1 h-4 bg-blue-600 rounded-full"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            {/* Shadow */}
                            <div className="absolute -bottom-2 -right-2 w-32 h-32 bg-black opacity-20 rounded-2xl transform rotate-6 -z-10"></div>
                        </div>
                    </div>
                </div>

                {/* Title */}
                <h1 className="text-xl font-bold mb-3 leading-tight">
                    Hệ thống đăng ký tín chỉ
                    <br />
                    <span className="text-blue-200">an toàn & tiện lợi</span>
                </h1>

                {/* Description */}
                <p className="text-blue-100 text-sm mb-6 leading-relaxed">
                    Tham gia cùng hàng triệu sinh viên thông minh đã tin tưởng chúng tôi
                    để quản lý việc đăng ký học phần. Đăng nhập để truy cập bảng điều
                    khiển cá nhân, theo dõi tiến độ học tập và đưa ra quyết định học tập
                    sáng suốt.
                </p>

                {/* Features */}
                <div className="space-y-2 text-left">
                    <FeatureItem text="Đăng ký học phần nhanh chóng" />
                    <FeatureItem text="Theo dõi tiến độ học tập" />
                    <FeatureItem text="Bảo mật thông tin cao" />
                    <FeatureItem text="Hỗ trợ 24/7" />
                </div>

                {/* Stats */}
                <div className="mt-8 grid grid-cols-2 gap-4 text-center">
                    <StatItem value="50K+" label="Sinh viên tin tưởng" />
                    <StatItem value="99.9%" label="Thời gian hoạt động" />
                </div>
            </div>

            {/* Floating Elements */}
            <FloatingDot
                position="top-6 right-6"
                size="w-2 h-2"
                color="bg-yellow-300"
            />
            <FloatingDot
                position="bottom-6 left-6"
                size="w-1.5 h-1.5"
                color="bg-blue-300"
                animationDelay={1000}
            />
            <FloatingDot
                position="top-1/3 left-4"
                size="w-3 h-3"
                color="bg-sky-300"
                animationDelay={500}
            />
        </div>
    );
}
