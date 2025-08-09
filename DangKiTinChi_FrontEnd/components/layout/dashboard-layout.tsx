"use client"

import { useState, useEffect } from "react"
import { cn } from "@/libs/utils"
import { DashboardSidebar } from "@/components/layout/dashboard-sidebar"
import { DashboardNavbar } from "@/components/layout/dashboard-navbar"
import { useIsMobile } from "@/hooks/use-mobile"

interface DashboardLayoutProps {
  children: React.ReactNode
  className?: string
}

export function DashboardLayout({ children, className }: DashboardLayoutProps) {
  const isMobile = useIsMobile()
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false)
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false)

  // Auto collapse sidebar on mobile
  useEffect(() => {
    if (isMobile) {
      setSidebarCollapsed(true)
      setMobileMenuOpen(false)
    } else {
      setSidebarCollapsed(false)
    }
  }, [isMobile])

  const toggleSidebar = () => {
    if (isMobile) {
      setMobileMenuOpen(!mobileMenuOpen)
    } else {
      setSidebarCollapsed(!sidebarCollapsed)
    }
  }

  const closeMobileMenu = () => {
    if (isMobile) {
      setMobileMenuOpen(false)
    }
  }

  return (
    <div className={cn("flex h-screen bg-gradient-to-br from-slate-50 to-cyan-50", className)}>
      {/* Mobile backdrop */}
      {isMobile && mobileMenuOpen && (
        <div
          className="fixed inset-0 bg-black/50 z-40 lg:hidden"
          onClick={closeMobileMenu}
        />
      )}

      {/* Desktop Sidebar */}
      {!isMobile && (
        <DashboardSidebar
          isCollapsed={sidebarCollapsed}
          onToggleCollapse={toggleSidebar}
          className="flex-shrink-0 transition-all duration-300"
        />
      )}

      {/* Mobile Sidebar */}
      {isMobile && (
        <DashboardSidebar
          isCollapsed={false}
          onToggleCollapse={closeMobileMenu}
          className={cn(
            "fixed left-0 top-0 h-full z-50 transition-transform duration-300 lg:hidden",
            mobileMenuOpen ? "translate-x-0" : "-translate-x-full"
          )}
        />
      )}

      <div className="flex flex-col flex-1 min-w-0">
        <DashboardNavbar
          onMenuClick={toggleSidebar}
          showMenuButton={isMobile}
        />

        <main className="flex-1 overflow-auto p-3 sm:p-4 lg:p-6">
          <div className="mx-auto">
            {children}
          </div>
        </main>
      </div>
    </div>
  )
}
