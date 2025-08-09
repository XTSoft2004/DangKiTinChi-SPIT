"use client"

import { ResponsiveGrid, ResponsiveCard, ResponsiveContainer } from "@/components/ui/responsive-layout"
import { Users, BookOpen, GraduationCap, TrendingUp, Calendar, Clock, Award, Target } from "lucide-react"
import { useResponsive } from "@/hooks/use-responsive"

const stats = [
  {
    title: "Tổng sinh viên",
    value: "2,543",
    change: "+12%",
    icon: Users,
    color: "text-blue-600",
    bgColor: "bg-blue-50",
  },
  {
    title: "Khóa học",
    value: "156",
    change: "+8%",
    icon: BookOpen,
    color: "text-green-600",
    bgColor: "bg-green-50",
  },
  {
    title: "Giảng viên",
    value: "89",
    change: "+5%",
    icon: GraduationCap,
    color: "text-purple-600",
    bgColor: "bg-purple-50",
  },
  {
    title: "Tỷ lệ tốt nghiệp",
    value: "94.2%",
    change: "+2.1%",
    icon: TrendingUp,
    color: "text-orange-600",
    bgColor: "bg-orange-50",
  },
]

const activities = [
  {
    title: "Đăng ký học phần",
    time: "2 giờ trước",
    description: "50 sinh viên đã đăng ký học phần mới",
    icon: Calendar,
  },
  {
    title: "Bài thi được tạo",
    time: "4 giờ trước",
    description: "Giảng viên Nguyễn Văn A tạo bài thi môn Lập trình Web",
    icon: Clock,
  },
  {
    title: "Chứng chỉ mới",
    time: "1 ngày trước",
    description: "20 sinh viên hoàn thành khóa học React.js",
    icon: Award,
  },
  {
    title: "Mục tiêu hoàn thành",
    time: "2 ngày trước",
    description: "Lớp CNTT-K16 đạt 95% tỷ lệ hoàn thành môn học",
    icon: Target,
  },
]

export default function DashboardExample() {
  const { isMobile, isTablet, isDesktop } = useResponsive()

  return (
    <ResponsiveContainer className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
        <div>
          <h1 className="text-2xl sm:text-3xl font-bold text-slate-900">Dashboard</h1>
          <p className="text-sm sm:text-base text-slate-600 mt-1">
            Chào mừng bạn quay trở lại! Đây là tổng quan về hệ thống.
          </p>
        </div>
        <div className="text-xs sm:text-sm text-slate-500">
          Cập nhật lần cuối: {new Date().toLocaleString('vi-VN')}
        </div>
      </div>

      {/* Stats Grid */}
      <ResponsiveGrid
        cols={{
          default: 1,
          sm: 2,
          lg: 4
        }}
        gap={{
          default: 4,
          md: 6
        }}
        className="flex flex-col lg:flex-row gap-4"
      >
        {stats.map((stat, index) => {
          const IconComponent = stat.icon
          return (
            <ResponsiveCard
              key={index}
              className="flex-1 hover:shadow-lg transition-all duration-200"
            >
              <div className="flex items-center justify-between">
                <div className="flex-1">
                  <p className="text-xs sm:text-sm font-medium text-slate-600 mb-1">
                    {stat.title}
                  </p>
                  <p className="text-xl sm:text-2xl font-bold text-slate-900 mb-1">
                    {stat.value}
                  </p>
                  <div className="flex items-center text-xs sm:text-sm">
                    <span className="text-green-600 font-medium">{stat.change}</span>
                    <span className="text-slate-500 ml-1">so với tháng trước</span>
                  </div>
                </div>
                <div className={`p-2 sm:p-3 rounded-lg ${stat.bgColor}`}>
                  <IconComponent className={`h-5 w-5 sm:h-6 sm:w-6 ${stat.color}`} />
                </div>
              </div>
            </ResponsiveCard>
          )
        })}
      </ResponsiveGrid>

      {/* Content Grid */}
      <ResponsiveGrid
        cols={{
          default: 1,
          lg: 3
        }}
        gap={{
          default: 4,
          md: 6
        }}
      >
        {/* Chart Card */}
        <div className="lg:col-span-2">
          <ResponsiveCard className="h-full">
            <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-4 sm:mb-6">
              <h3 className="text-base sm:text-lg font-semibold text-slate-900">
                Thống kê đăng ký
              </h3>
              <div className="flex gap-2 mt-2 sm:mt-0">
                <button className="px-3 py-1 text-xs sm:text-sm bg-cyan-100 text-cyan-700 rounded-md">
                  7 ngày
                </button>
                <button className="px-3 py-1 text-xs sm:text-sm text-slate-600 hover:bg-slate-100 rounded-md">
                  30 ngày
                </button>
              </div>
            </div>
            <div className="h-48 sm:h-64 bg-slate-50 rounded-lg flex items-center justify-center">
              <p className="text-slate-500 text-sm">Biểu đồ thống kê</p>
            </div>
          </ResponsiveCard>
        </div>

        {/* Activity Card */}
        <ResponsiveCard>
          <h3 className="text-base sm:text-lg font-semibold text-slate-900 mb-4">
            Hoạt động gần đây
          </h3>
          <div className="space-y-3 sm:space-y-4">
            {activities.map((activity, index) => {
              const IconComponent = activity.icon
              return (
                <div key={index} className="flex gap-3 sm:gap-4 p-2 sm:p-3 hover:bg-slate-50 rounded-lg transition-colors">
                  <div className="p-2 bg-cyan-50 rounded-lg flex-shrink-0">
                    <IconComponent className="h-4 w-4 text-cyan-600" />
                  </div>
                  <div className="flex-1 min-w-0">
                    <h4 className="text-sm font-medium text-slate-900 mb-1">
                      {activity.title}
                    </h4>
                    <p className="text-xs sm:text-sm text-slate-600 mb-1">
                      {activity.description}
                    </p>
                    <p className="text-xs text-slate-500">{activity.time}</p>
                  </div>
                </div>
              )
            })}
          </div>
        </ResponsiveCard>
      </ResponsiveGrid>

      {/* Quick Actions */}
      <ResponsiveCard>
        <h3 className="text-base sm:text-lg font-semibold text-slate-900 mb-4">
          Thao tác nhanh
        </h3>
        <ResponsiveGrid
          cols={{
            default: 2,
            sm: 4,
            lg: 6
          }}
          gap={{
            default: 3,
            sm: 4
          }}
        >
          {[
            { label: "Thêm sinh viên", icon: Users },
            { label: "Tạo khóa học", icon: BookOpen },
            { label: "Xem báo cáo", icon: TrendingUp },
            { label: "Quản lý lịch", icon: Calendar },
            { label: "Cài đặt", icon: Target },
            { label: "Hỗ trợ", icon: Award },
          ].map((action, index) => {
            const IconComponent = action.icon
            return (
              <button
                key={index}
                className="flex flex-col items-center gap-2 p-3 sm:p-4 border border-slate-200 rounded-lg hover:border-cyan-200 hover:bg-cyan-50 transition-all duration-200 group"
              >
                <IconComponent className="h-5 w-5 sm:h-6 sm:w-6 text-slate-600 group-hover:text-cyan-600" />
                <span className="text-xs sm:text-sm font-medium text-slate-700 text-center">
                  {action.label}
                </span>
              </button>
            )
          })}
        </ResponsiveGrid>
      </ResponsiveCard>

      {/* Debug info - chỉ hiển thị khi development */}
      {/* {process.env.NODE_ENV === 'development' && (
        <div className="fixed bottom-4 right-4 bg-black/80 text-white p-2 rounded text-xs">
          {isMobile && "Mobile"}
          {isTablet && "Tablet"}
          {isDesktop && "Desktop"}
        </div>
      )} */}
    </ResponsiveContainer>
  )
}
