import {
  GraduationCap,
  BookOpen,
  Calendar,
  Users,
  TrendingUp,
  Star,
} from "lucide-react";

interface AuthContentProps {
  isLogin: boolean;
}

export function AuthContent({ isLogin }: AuthContentProps) {
  if (isLogin) {
    return (
      <div className="flex flex-col justify-center h-full p-12">
        <div className="mb-8">
          <div className="flex items-center space-x-3 mb-6">
            <div className="p-3 rounded-2xl bg-gradient-to-br from-white/20 to-white/30">
              <GraduationCap className="h-8 w-8 text-white" />
            </div>
            <span className="text-3xl font-bold text-white">
              SPIT System
            </span>
          </div>
          <h1 className="text-4xl font-bold text-white mb-4 leading-tight">
            Hệ thống quản lý
            <br />
            <span className="text-cyan-100">đăng ký tín chỉ</span>
          </h1>
          <p className="text-xl text-white/90 leading-relaxed">
            Đăng ký môn học, theo dõi lịch học và quản lý kết quả học tập một
            cách dễ dàng và hiệu quả.
          </p>
        </div>

        <div className="space-y-6">
          <div className="flex items-center space-x-4 p-4 rounded-xl bg-white/15 backdrop-blur border border-white/20">
            <div className="p-2 rounded-lg bg-white/20">
              <BookOpen className="h-5 w-5 text-white" />
            </div>
            <div>
              <h3 className="font-semibold text-white">Đăng ký môn học</h3>
              <p className="text-sm text-white/80">
                Dễ dàng chọn và đăng ký các môn học phù hợp
              </p>
            </div>
          </div>

          <div className="flex items-center space-x-4 p-4 rounded-xl bg-white/15 backdrop-blur border border-white/20">
            <div className="p-2 rounded-lg bg-white/20">
              <Calendar className="h-5 w-5 text-white" />
            </div>
            <div>
              <h3 className="font-semibold text-white">Quản lý lịch học</h3>
              <p className="text-sm text-white/80">
                Theo dõi thời khóa biểu và lịch thi một cách chi tiết
              </p>
            </div>
          </div>

          <div className="flex items-center space-x-4 p-4 rounded-xl bg-white/15 backdrop-blur border border-white/20">
            <div className="p-2 rounded-lg bg-white/20">
              <TrendingUp className="h-5 w-5 text-white" />
            </div>
            <div>
              <h3 className="font-semibold text-white">Theo dõi kết quả</h3>
              <p className="text-sm text-white/80">
                Xem điểm số và tiến độ học tập của bạn
              </p>
            </div>
          </div>
        </div>

        <div className="mt-12 flex items-center space-x-4">
          <div className="flex -space-x-2">
            <div className="w-10 h-10 rounded-full bg-gradient-to-br from-pink-400 to-pink-600 border-2 border-white"></div>
            <div className="w-10 h-10 rounded-full bg-gradient-to-br from-purple-400 to-purple-600 border-2 border-white"></div>
            <div className="w-10 h-10 rounded-full bg-gradient-to-br from-blue-400 to-blue-600 border-2 border-white"></div>
            <div className="w-10 h-10 rounded-full bg-gradient-to-br from-green-400 to-green-600 border-2 border-white flex items-center justify-center">
              <span className="text-xs font-bold text-white">+</span>
            </div>
          </div>
          <div>
            <p className="text-white/90 text-sm">
              <span className="font-semibold text-white">5,000+</span> sinh viên
              đang sử dụng
            </p>
            <div className="flex items-center space-x-1">
              {[...Array(5)].map((_, i) => (
                <Star
                  key={i}
                  className="h-3 w-3 fill-yellow-400 text-yellow-400"
                />
              ))}
              <span className="text-xs text-white/80 ml-1">4.9/5 đánh giá</span>
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="flex flex-col justify-center h-full p-12">
      <div className="mb-8">
        <h1 className="text-4xl font-bold text-white mb-4 leading-tight">
          Tham gia cộng đồng
          <br />
          <span className="text-cyan-100">sinh viên SPIT</span>
        </h1>
        <p className="text-xl text-white/90 leading-relaxed">
          Kết nối với hàng ngàn sinh viên khác và tận hưởng trải nghiệm học tập
          tuyệt vời.
        </p>
      </div>

      <div className="grid grid-cols-2 gap-6 mb-8">
        <div className="text-center p-6 rounded-xl bg-white/15 backdrop-blur border border-white/20">
          <div className="text-3xl font-bold text-white mb-2">5,000+</div>
          <div className="text-white/80 text-sm">Sinh viên đang sử dụng</div>
        </div>
        <div className="text-center p-6 rounded-xl bg-white/15 backdrop-blur border border-white/20">
          <div className="text-3xl font-bold text-white mb-2">1,200+</div>
          <div className="text-white/80 text-sm">Môn học có sẵn</div>
        </div>
        <div className="text-center p-6 rounded-xl bg-white/15 backdrop-blur border border-white/20">
          <div className="text-3xl font-bold text-white mb-2">50+</div>
          <div className="text-white/80 text-sm">Chuyên ngành</div>
        </div>
        <div className="text-center p-6 rounded-xl bg-white/15 backdrop-blur border border-white/20">
          <div className="text-3xl font-bold text-white mb-2">24/7</div>
          <div className="text-white/80 text-sm">Hỗ trợ sinh viên</div>
        </div>
      </div>

      <div className="space-y-4">
        <h3 className="text-xl font-semibold text-white mb-4">
          Tính năng nổi bật
        </h3>

        <div className="flex items-center space-x-4 p-4 rounded-xl bg-white/10">
          <Users className="h-5 w-5 text-white" />
          <div>
            <h4 className="font-medium text-white">Cộng đồng học tập</h4>
            <p className="text-sm text-white/80">
              Kết nối và học tập cùng bạn bè
            </p>
          </div>
        </div>

        <div className="flex items-center space-x-4 p-4 rounded-xl bg-white/10">
          <BookOpen className="h-5 w-5 text-white" />
          <div>
            <h4 className="font-medium text-white">Thư viện tài liệu</h4>
            <p className="text-sm text-white/80">
              Truy cập hàng ngàn tài liệu học tập
            </p>
          </div>
        </div>

        <div className="flex items-center space-x-4 p-4 rounded-xl bg-white/10">
          <Calendar className="h-5 w-5 text-white" />
          <div>
            <h4 className="font-medium text-white">Nhắc nhở thông minh</h4>
            <p className="text-sm text-white/80">
              Không bao giờ quên deadline quan trọng
            </p>
          </div>
        </div>
      </div>

      <div className="mt-8 p-6 rounded-xl bg-gradient-to-r from-cyan-600/30 to-teal-600/30 border border-white/30">
        <h4 className="font-semibold text-white mb-2">💡 Bạn có biết?</h4>
        <p className="text-white/90 text-sm">
          Sinh viên sử dụng SPIT có tỷ lệ hoàn thành khóa học cao hơn 85% so với
          hệ thống truyền thống.
        </p>
      </div>
    </div>
  );
}
