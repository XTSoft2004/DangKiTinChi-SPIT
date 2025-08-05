"use client"

import { useState } from "react"
import { cn } from "@/libs/utils"
import { DashboardSidebar } from "@/components/layout/dashboard-sidebar"
import { DashboardNavbar } from "@/components/layout/dashboard-navbar"

interface DashboardLayoutProps {
  children: React.ReactNode
  className?: string
}

export function DashboardLayout({ children, className }: DashboardLayoutProps) {
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false)

  const toggleSidebar = () => {
    setSidebarCollapsed(!sidebarCollapsed)
  }

  return (
    <div className={cn("flex h-screen bg-gradient-to-br from-slate-50 to-cyan-50", className)}>
      <DashboardSidebar
        isCollapsed={sidebarCollapsed}
        onToggleCollapse={toggleSidebar}
        className="flex-shrink-0 transition-all duration-300"
      />

      <div className="flex flex-col flex-1 min-w-0">
        <DashboardNavbar />

        <main className="flex-1 overflow-auto p-6">
          <div className="max-w-7xl mx-auto">
            {children}
          </div>
        </main>
      </div>
    </div>
  )
}
