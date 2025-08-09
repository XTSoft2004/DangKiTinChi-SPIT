"use client";

import { useState, useEffect } from "react";
import { cn } from "@/libs/utils";
import { Button } from "@/components/ui/shadcn-ui/button";
import {
  Home,
  Users,
  Settings,
  ChevronLeft,
  ChevronRight,
  ChevronDown,
  ChevronUp,
  GraduationCap,
} from "lucide-react";
import { useRouter, usePathname } from "next/navigation";

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
    icon: Home,
    label: "Trang chủ",
    href: "/dashboard",
  },
  {
    icon: Settings,
    label: "Quản lý",
    children: [
      {
        icon: Users,
        label: "Người dùng",
        href: "/admin/user",
      },
      {
        icon: Users,
        label: "Tài khoản tín chỉ",
        href: "/admin/account",
      },
    ],
  },

];

const SidebarHeader = ({
  isCollapsed,
  onToggleCollapse,
}: {
  isCollapsed: boolean;
  onToggleCollapse: () => void;
}) => (
  <div className="flex items-center justify-between p-3 sm:p-4">
    {!isCollapsed && (
      <div className="flex items-center space-x-2 sm:space-x-3">
        <div className="p-1.5 sm:p-2 rounded-lg bg-gradient-to-br from-cyan-400 to-cyan-600">
          <GraduationCap className="h-4 w-4 sm:h-5 sm:w-5 text-white" />
        </div>
        <span className="font-bold text-base sm:text-lg bg-gradient-to-r from-cyan-500 to-cyan-700 bg-clip-text text-transparent">
          SPIT
        </span>
      </div>
    )}
    <Button
      variant="ghost"
      size="icon"
      onClick={onToggleCollapse}
      className="h-7 w-7 sm:h-8 sm:w-8 rounded-full hover:bg-cyan-50 transition-colors"
    >
      {isCollapsed ? (
        <ChevronRight className="h-3 w-3 sm:h-4 sm:w-4 text-cyan-600" />
      ) : (
        <ChevronLeft className="h-3 w-3 sm:h-4 sm:w-4 text-cyan-600" />
      )}
    </Button>
  </div>
);

const SidebarFooter = ({ isCollapsed }: { isCollapsed: boolean }) => (
  <>
    {!isCollapsed && (
      <div className="p-3 sm:p-4 mt-auto border-t border-cyan-100/50">
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
        "w-full justify-start h-8 sm:h-9 rounded-lg transition-all duration-200 px-2 sm:px-3",
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
            "h-3 w-3 sm:h-4 sm:w-4 mr-2 sm:mr-3 transition-colors",
            isActive ? "text-cyan-600" : "text-slate-400"
          )}
        />
      )}
      <span className="text-xs sm:text-sm">{child.label}</span>
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
          "w-full justify-start h-10 sm:h-11 rounded-xl transition-all duration-200",
          isCollapsed ? "px-2 sm:px-3" : "px-3 sm:px-4",
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
              "h-4 w-4 sm:h-5 sm:w-5 transition-colors",
              isCollapsed ? "mr-0" : "mr-2 sm:mr-3",
              isActive ? "text-cyan-600" : "text-slate-500"
            )}
          />
        )}
        {!isCollapsed && (
          <>
            <span className="text-xs sm:text-sm font-medium flex-1 text-left">
              {item.label}
            </span>
            {hasChildren && (
              <span className="ml-auto">
                {isOpen ? (
                  <ChevronUp className="h-3 w-3 sm:h-4 sm:w-4" />
                ) : (
                  <ChevronDown className="h-3 w-3 sm:h-4 sm:w-4" />
                )}
              </span>
            )}
          </>
        )}
      </Button>

      {/* Submenu */}
      {hasChildren && isOpen && !isCollapsed && (
        <div className="ml-4 sm:ml-6 mt-1 space-y-1">
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
  const pathname = usePathname();
  const [activeItem, setActiveItem] = useState("");
  const [openMenus, setOpenMenus] = useState<string[]>([]);

  // Update active item based on current pathname
  useEffect(() => {
    setActiveItem(pathname);

    // Auto-open parent menus for nested routes
    const findParentMenu = (items: MenuItem[]): string[] => {
      const openMenus: string[] = [];

      const searchInItems = (menuItems: MenuItem[]) => {
        menuItems.forEach(item => {
          if (item.children) {
            const hasActiveChild = item.children.some(child => child.href === pathname);
            if (hasActiveChild) {
              openMenus.push(item.label);
            }
            searchInItems(item.children);
          }
        });
      };

      searchInItems(items);
      return openMenus;
    };

    const menusToOpen = findParentMenu(menuItems);
    setOpenMenus(menusToOpen);
  }, [pathname]);

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
        isCollapsed ? "w-14 sm:w-16" : "w-56 sm:w-64",
        className
      )}
    >
      <SidebarHeader
        isCollapsed={isCollapsed}
        onToggleCollapse={onToggleCollapse}
      />

      <nav className="flex-1 px-2 sm:px-3 py-2">
        <div className="space-y-1 sm:space-y-2">
          {menuItems.map((item) => {
            const isActive = activeItem === item.href;
            const hasChildren = Boolean(
              item.children && item.children.length > 0
            );
            const isOpen = openMenus.includes(item.label);

            // Check if any child is active for parent highlighting
            const hasActiveChild = hasChildren && Boolean(item.children?.some(child => child.href === activeItem));
            const isParentActive = hasActiveChild && !isActive;

            return (
              <MainMenuItem
                key={item.href || item.label}
                item={item}
                isCollapsed={isCollapsed}
                isActive={isActive || isParentActive}
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
