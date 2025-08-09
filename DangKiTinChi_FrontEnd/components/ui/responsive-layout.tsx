"use client"

import { cn } from "@/libs/utils"
import { ReactNode } from "react"

interface ResponsiveGridProps {
    children: ReactNode
    className?: string
    cols?: {
        default?: number
        sm?: number
        md?: number
        lg?: number
        xl?: number
        "2xl"?: number
    }
    gap?: {
        default?: number | string
        sm?: number | string
        md?: number | string
        lg?: number | string
        xl?: number | string
        "2xl"?: number | string
    }
}

export function ResponsiveGrid({
    children,
    className,
    cols = { default: 1, sm: 2, lg: 3, xl: 4 },
    gap = { default: 4, md: 6 }
}: ResponsiveGridProps) {
    const getColsClass = () => {
        const classes = []

        if (cols.default) classes.push(`grid-cols-${cols.default}`)
        if (cols.sm) classes.push(`sm:grid-cols-${cols.sm}`)
        if (cols.md) classes.push(`md:grid-cols-${cols.md}`)
        if (cols.lg) classes.push(`lg:grid-cols-${cols.lg}`)
        if (cols.xl) classes.push(`xl:grid-cols-${cols.xl}`)
        if (cols["2xl"]) classes.push(`2xl:grid-cols-${cols["2xl"]}`)

        return classes.join(" ")
    }

    const getGapClass = () => {
        const classes = []

        if (gap.default) classes.push(`gap-${gap.default}`)
        if (gap.sm) classes.push(`sm:gap-${gap.sm}`)
        if (gap.md) classes.push(`md:gap-${gap.md}`)
        if (gap.lg) classes.push(`lg:gap-${gap.lg}`)
        if (gap.xl) classes.push(`xl:gap-${gap.xl}`)
        if (gap["2xl"]) classes.push(`2xl:gap-${gap["2xl"]}`)

        return classes.join(" ")
    }

    return (
        <div className={cn(
            "grid",
            getColsClass(),
            getGapClass(),
            className
        )}>
            {children}
        </div>
    )
}

interface ResponsiveCardProps {
    children: ReactNode
    className?: string
    padding?: {
        default?: number | string
        sm?: number | string
        md?: number | string
        lg?: number | string
    }
}

export function ResponsiveCard({
    children,
    className,
    padding = { default: 4, md: 6 }
}: ResponsiveCardProps) {
    const getPaddingClass = () => {
        const classes = []

        if (padding.default) classes.push(`p-${padding.default}`)
        if (padding.sm) classes.push(`sm:p-${padding.sm}`)
        if (padding.md) classes.push(`md:p-${padding.md}`)
        if (padding.lg) classes.push(`lg:p-${padding.lg}`)

        return classes.join(" ")
    }

    return (
        <div className={cn(
            "bg-white rounded-lg shadow-sm border border-slate-200/60 transition-all hover:shadow-md",
            getPaddingClass(),
            className
        )}>
            {children}
        </div>
    )
}

interface ResponsiveContainerProps {
    children: ReactNode
    className?: string
    maxWidth?: "sm" | "md" | "lg" | "xl" | "2xl" | "7xl" | "full"
    padding?: {
        x?: {
            default?: number | string
            sm?: number | string
            md?: number | string
            lg?: number | string
        }
        y?: {
            default?: number | string
            sm?: number | string
            md?: number | string
            lg?: number | string
        }
    }
}

export function ResponsiveContainer({
    children,
    className,
    maxWidth = "full",
    padding = {
        x: { default: 4, sm: 6, lg: 8 },
        y: { default: 4, md: 6 }
    }
}: ResponsiveContainerProps) {
    const getMaxWidthClass = () => {
        const maxWidthMap = {
            sm: "max-w-sm",
            md: "max-w-md",
            lg: "max-w-lg",
            xl: "max-w-xl",
            "2xl": "max-w-2xl",
            "7xl": "max-w-7xl",
            full: "max-w-full"
        }
        return maxWidthMap[maxWidth] || "max-w-7xl"
    }

    const getPaddingClass = () => {
        const classes = []

        // X padding
        if (padding.x?.default) classes.push(`px-${padding.x.default}`)
        if (padding.x?.sm) classes.push(`sm:px-${padding.x.sm}`)
        if (padding.x?.md) classes.push(`md:px-${padding.x.md}`)
        if (padding.x?.lg) classes.push(`lg:px-${padding.x.lg}`)

        // Y padding
        if (padding.y?.default) classes.push(`py-${padding.y.default}`)
        if (padding.y?.sm) classes.push(`sm:py-${padding.y.sm}`)
        if (padding.y?.md) classes.push(`md:py-${padding.y.md}`)
        if (padding.y?.lg) classes.push(`lg:py-${padding.y.lg}`)

        return classes.join(" ")
    }

    return (
        <div className={cn(
            "mx-auto w-full",
            getMaxWidthClass(),
            getPaddingClass(),
            className
        )}>
            {children}
        </div>
    )
}
