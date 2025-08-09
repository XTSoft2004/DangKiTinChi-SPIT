"use client";

import { Button } from "@/components/ui/shadcn-ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/shadcn-ui/dropdown-menu";
import { Avatar } from "@/components/ui/shadcn-ui/avatar";
import { LogOut, ChevronDown, User, Menu } from "lucide-react";
import { cn } from "@/libs/utils";

interface DashboardNavbarProps {
  className?: string;
  userName?: string;
  userAvatar?: string;
  onMenuClick?: () => void;
  showMenuButton?: boolean;
}

export function DashboardNavbar({
  className,
  userName = "Nguyễn Văn A",
  userAvatar,
  onMenuClick,
  showMenuButton = false,
}: DashboardNavbarProps) {
  const handleLogout = () => {
    console.log("Logout clicked");
  };

  return (
    <header
      className={cn(
        "flex items-center justify-between px-4 sm:px-6 py-1 sm:py-3 bg-white/95 backdrop-blur border-b border-cyan-100/50 shadow-sm",
        className
      )}
    >
      <div className="flex items-center space-x-3">
        {/* Mobile Menu Button */}
        {showMenuButton && (
          <Button
            variant="ghost"
            size="icon"
            onClick={onMenuClick}
            className="h-9 w-9 rounded-lg hover:bg-cyan-50 transition-colors lg:hidden"
          >
            <Menu className="h-5 w-5 text-slate-600" />
          </Button>
        )}

        <h1 className="text-lg sm:text-xl font-semibold text-slate-800">
          Dashboard
        </h1>
      </div>

      <div className="flex items-center">
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button
              variant="ghost"
              className="h-9 sm:h-10 px-2 sm:px-3 space-x-1 sm:space-x-2 rounded-full hover:bg-cyan-50 transition-colors"
            >
              <Avatar className="h-6 w-6 sm:h-7 sm:w-7">
                {userAvatar ? (
                  <img
                    src={userAvatar}
                    alt={userName}
                    className="h-6 w-6 sm:h-7 sm:w-7 rounded-full object-cover"
                  />
                ) : (
                  <div className="h-6 w-6 sm:h-7 sm:w-7 rounded-full bg-gradient-to-br from-cyan-400 to-cyan-600 flex items-center justify-center">
                    <User className="h-3 w-3 sm:h-4 sm:w-4 text-white" />
                  </div>
                )}
              </Avatar>
              <span className="text-xs sm:text-sm font-medium hidden xs:block sm:block text-slate-700 max-w-24 sm:max-w-none truncate">
                {userName}
              </span>
              <ChevronDown className="h-3 w-3 text-slate-500 hidden xs:block" />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent
            align="end"
            className="w-48 sm:w-56 shadow-lg border border-cyan-100/50 bg-white/95 backdrop-blur"
          >
            <DropdownMenuLabel className="font-normal">
              <div className="flex flex-col space-y-1">
                <p className="text-sm font-medium leading-none text-slate-800">
                  {userName}
                </p>
                <p className="text-xs leading-none text-slate-500">
                  student@university.edu
                </p>
              </div>
            </DropdownMenuLabel>
            <DropdownMenuSeparator className="bg-cyan-100/50" />
            <DropdownMenuItem className="cursor-pointer hover:bg-cyan-50 transition-colors">
              <User className="mr-2 h-4 w-4 text-cyan-600" />
              <span className="text-slate-700">Hồ sơ cá nhân</span>
            </DropdownMenuItem>
            <DropdownMenuSeparator className="bg-cyan-100/50" />
            <DropdownMenuItem
              onClick={handleLogout}
              className="cursor-pointer text-red-600 focus:text-red-600 hover:bg-red-50 transition-colors"
            >
              <LogOut className="mr-2 h-4 w-4" />
              <span>Đăng xuất</span>
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
    </header>
  );
}
