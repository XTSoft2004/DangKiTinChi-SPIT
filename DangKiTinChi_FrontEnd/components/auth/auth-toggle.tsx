"use client"

import { Button } from "@/components/ui/shadcn-ui/button"
import { LogIn, UserPlus } from "lucide-react"

interface AuthToggleProps {
  isLogin: boolean
  onToggle: () => void
}

export function AuthToggle({ isLogin, onToggle }: AuthToggleProps) {
  return (
    <div className="fixed top-6 right-6 z-50">
      <div className="flex items-center space-x-2 bg-white/90 backdrop-blur rounded-full p-1 shadow-lg border border-cyan-100/50">
        <Button
          onClick={onToggle}
          variant={isLogin ? "default" : "ghost"}
          size="sm"
          className={`rounded-full h-10 px-4 transition-all duration-200 ${
            isLogin 
              ? "bg-gradient-to-r from-cyan-500 to-cyan-600 text-white shadow-md" 
              : "text-slate-600 hover:bg-cyan-50"
          }`}
        >
          <LogIn className="h-4 w-4 mr-2" />
          Đăng nhập
        </Button>
        <Button
          onClick={onToggle}
          variant={!isLogin ? "default" : "ghost"}
          size="sm"
          className={`rounded-full h-10 px-4 transition-all duration-200 ${
            !isLogin 
              ? "bg-gradient-to-r from-cyan-500 to-cyan-600 text-white shadow-md" 
              : "text-slate-600 hover:bg-cyan-50"
          }`}
        >
          <UserPlus className="h-4 w-4 mr-2" />
          Đăng ký
        </Button>
      </div>
    </div>
  )
}
