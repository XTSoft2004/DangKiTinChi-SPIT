"use client";

import { useState } from "react";
import { cn } from "@/libs/utils";
import { Button } from "@/components/ui/shadcn-ui/button";
import {
  Home,
  BookOpen,
  Users,
  Settings,
  ChevronLeft,
  ChevronRight,
  ChevronDown,
  ChevronUp,
  GraduationCap,
} from "lucide-react";
import { useRouter } from "next/navigation";

interface DashboardSidebarProps {
  isCollapsed: boolean;
  onToggleCollapse: () => void;
  className?: string;
}

interface MenuItem {
  icon?: React.ElementType;
  label: string;
  href?: string;
  children?: MenuItem[];
}

const menuItems: MenuItem[] = [
  {
    icon: Settings,
    label: "Quản lý",
    children: [
      {
        icon: Users,
        label: "Người dùng",
        href: "/user",
      },
    ],
  },
  {
    icon: Home,
    label: "Trang chủ",
    href: "/",
  },
];

const SidebarHeader = ({
  isCollapsed,
  onToggleCollapse,
}: {
  isCollapsed: boolean;
  onToggleCollapse: () => void;
}) => (
  <div className="flex items-center justify-between p-4">
    {!isCollapsed && (
      <div className="flex items-center space-x-3">
        <div className="p-2 rounded-lg bg-gradient-to-br from-cyan-400 to-cyan-600">
          <GraduationCap className="h-5 w-5 text-white" />
        </div>
        <span className="font-bold text-lg bg-gradient-to-r from-cyan-500 to-cyan-700 bg-clip-text text-transparent">
          SPIT
        </span>
      </div>
    )}
    <Button
      variant="ghost"
      size="icon"
      onClick={onToggleCollapse}
      className="h-8 w-8 rounded-full hover:bg-cyan-50 transition-colors"
    >
      {isCollapsed ? (
        <ChevronRight className="h-4 w-4 text-cyan-600" />
      ) : (
        <ChevronLeft className="h-4 w-4 text-cyan-600" />
      )}
    </Button>
  </div>
);

const SidebarFooter = ({ isCollapsed }: { isCollapsed: boolean }) => (
  <>
    {!isCollapsed && (
      <div className="p-4 mt-auto border-t border-cyan-100/50">
        <div className="text-xs text-slate-400 text-center">Version 1.0.0</div>
      </div>
    )}
  </>
);

const SubMenuItem = ({
  child,
  isActive,
  onItemClick,
}: {
  child: MenuItem;
  isActive: boolean;
  onItemClick: (href: string) => void;
}) => {
  const ChildIcon = child.icon;

  return (
    <Button
      key={child.href || child.label}
      variant="ghost"
      className={cn(
        "w-full justify-start h-9 rounded-lg transition-all duration-200 px-3",
        isActive
          ? "bg-gradient-to-r from-cyan-500/10 to-cyan-400/10 text-cyan-700 shadow-sm"
          : "hover:bg-cyan-50 text-slate-500 hover:text-cyan-700"
      )}
      onClick={() => child.href && onItemClick(child.href)}
      disabled={!child.href}
    >
      {ChildIcon && (
        <ChildIcon
          className={cn(
            "h-4 w-4 mr-3 transition-colors",
            isActive ? "text-cyan-600" : "text-slate-400"
          )}
        />
      )}
      <span className="text-sm">{child.label}</span>
    </Button>
  );
};

const MainMenuItem = ({
  item,
  isCollapsed,
  isActive,
  isOpen,
  hasChildren,
  activeItem,
  onItemClick,
  onToggleSubmenu,
}: {
  item: MenuItem;
  isCollapsed: boolean;
  isActive: boolean;
  isOpen: boolean;
  hasChildren: boolean;
  activeItem: string;
  onItemClick: (href: string) => void;
  onToggleSubmenu: (label: string) => void;
}) => {
  const Icon = item.icon;

  const handleClick = () => {
    if (hasChildren) {
      onToggleSubmenu(item.label);
    } else if (item.href) {
      onItemClick(item.href);
    }
  };

  return (
    <div>
      <Button
        variant="ghost"
        className={cn(
          "w-full justify-start h-11 rounded-xl transition-all duration-200",
          isCollapsed ? "px-3" : "px-4",
          isActive
            ? "bg-gradient-to-r from-cyan-500/10 to-cyan-400/10 text-cyan-700 shadow-sm hover:bg-gradient-to-r hover:from-cyan-500/15 hover:to-cyan-400/15"
            : "hover:bg-cyan-50 text-slate-600 hover:text-cyan-700"
        )}
        onClick={handleClick}
        disabled={!item.href && !hasChildren}
      >
        {Icon && (
          <Icon
            className={cn(
              "h-5 w-5 transition-colors",
              isCollapsed ? "mr-0" : "mr-3",
              isActive ? "text-cyan-600" : "text-slate-500"
            )}
          />
        )}
        {!isCollapsed && (
          <>
            <span className="text-sm font-medium flex-1 text-left">
              {item.label}
            </span>
            {hasChildren && (
              <span className="ml-auto">
                {isOpen ? (
                  <ChevronUp className="h-4 w-4" />
                ) : (
                  <ChevronDown className="h-4 w-4" />
                )}
              </span>
            )}
          </>
        )}
      </Button>

      {/* Submenu */}
      {hasChildren && isOpen && !isCollapsed && (
        <div className="ml-6 mt-1 space-y-1">
          {item.children?.map((child) => (
            <SubMenuItem
              key={child.href || child.label}
              child={child}
              isActive={activeItem === child.href}
              onItemClick={onItemClick}
            />
          ))}
        </div>
      )}
    </div>
  );
};

export function DashboardSidebar({
  isCollapsed,
  onToggleCollapse,
  className,
}: DashboardSidebarProps) {
  const router = useRouter();
  const [activeItem, setActiveItem] = useState("/");
  const [openMenus, setOpenMenus] = useState<string[]>([]);

  const handleItemClick = (itemHref: string) => {
    setActiveItem(itemHref);
    router.push(itemHref);
    if (isCollapsed) {
      onToggleCollapse();
    }
  };

  const toggleSubmenu = (label: string) => {
    setOpenMenus((prev) =>
      prev.includes(label)
        ? prev.filter((item) => item !== label)
        : [...prev, label]
    );
  };

  return (
    <div
      className={cn(
        "relative flex flex-col h-full bg-white/95 backdrop-blur shadow-lg transition-all duration-300",
        isCollapsed ? "w-16" : "w-64",
        className
      )}
    >
      <SidebarHeader
        isCollapsed={isCollapsed}
        onToggleCollapse={onToggleCollapse}
      />

      <nav className="flex-1 px-3 py-2">
        <div className="space-y-2">
          {menuItems.map((item) => {
            const isActive = activeItem === item.href;
            const hasChildren = Boolean(
              item.children && item.children.length > 0
            );
            const isOpen = openMenus.includes(item.label);

            return (
              <MainMenuItem
                key={item.href || item.label}
                item={item}
                isCollapsed={isCollapsed}
                isActive={isActive}
                isOpen={isOpen}
                hasChildren={hasChildren}
                activeItem={activeItem}
                onItemClick={handleItemClick}
                onToggleSubmenu={toggleSubmenu}
              />
            );
          })}
        </div>
      </nav>

      <SidebarFooter isCollapsed={isCollapsed} />
    </div>
  );
}
