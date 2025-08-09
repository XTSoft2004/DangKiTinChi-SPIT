import * as React from "react"

export const BREAKPOINTS = {
  xs: 475,
  sm: 640, 
  md: 768,
  lg: 1024,
  xl: 1280,
  "2xl": 1536,
} as const

type Breakpoint = keyof typeof BREAKPOINTS

export function useResponsive() {
  const [screenWidth, setScreenWidth] = React.useState<number | undefined>(undefined)

  React.useEffect(() => {
    const updateScreenWidth = () => {
      setScreenWidth(window.innerWidth)
    }

    updateScreenWidth()
    window.addEventListener("resize", updateScreenWidth)
    return () => window.removeEventListener("resize", updateScreenWidth)
  }, [])

  const isBreakpoint = React.useCallback((breakpoint: Breakpoint, direction: 'up' | 'down' = 'up') => {
    if (screenWidth === undefined) return false
    
    const breakpointValue = BREAKPOINTS[breakpoint]
    
    if (direction === 'up') {
      return screenWidth >= breakpointValue
    } else {
      return screenWidth < breakpointValue
    }
  }, [screenWidth])

  return {
    screenWidth,
    isXs: isBreakpoint('xs', 'up'),
    isSm: isBreakpoint('sm', 'up'),
    isMd: isBreakpoint('md', 'up'),
    isLg: isBreakpoint('lg', 'up'),
    isXl: isBreakpoint('xl', 'up'),
    is2Xl: isBreakpoint('2xl', 'up'),
    isMobile: !isBreakpoint('md', 'up'),
    isTablet: isBreakpoint('md', 'up') && !isBreakpoint('lg', 'up'),
    isDesktop: isBreakpoint('lg', 'up'),
    isBreakpoint,
  }
}

export function useBreakpoint(breakpoint: Breakpoint, direction: 'up' | 'down' = 'up') {
  const [matches, setMatches] = React.useState<boolean | undefined>(undefined)

  React.useEffect(() => {
    const breakpointValue = BREAKPOINTS[breakpoint]
    const query = direction === 'up' 
      ? `(min-width: ${breakpointValue}px)`
      : `(max-width: ${breakpointValue - 1}px)`
    
    const mql = window.matchMedia(query)
    const onChange = () => setMatches(mql.matches)
    
    onChange()
    mql.addEventListener("change", onChange)
    return () => mql.removeEventListener("change", onChange)
  }, [breakpoint, direction])

  return !!matches
}
