"use client";

import { useState } from "react";
import { LoginForm } from "@/components/auth/login-form";
import { RegisterForm } from "@/components/auth/register-form";
import { AuthContent } from "@/components/auth/auth-content";
import { AuthToggle } from "@/components/auth/auth-toggle";
import { motion, AnimatePresence } from "framer-motion";

export default function AuthPageAnimated() {
  const [isLogin, setIsLogin] = useState(true);

  const switchToRegister = () => setIsLogin(false);
  const switchToLogin = () => setIsLogin(true);
  const toggleAuth = () => setIsLogin(!isLogin);

  const slideVariants = {
    enter: (direction: number) => ({
      x: direction > 0 ? 1000 : -1000,
      opacity: 0,
    }),
    center: {
      zIndex: 1,
      x: 0,
      opacity: 1,
    },
    exit: (direction: number) => ({
      zIndex: 0,
      x: direction < 0 ? 1000 : -1000,
      opacity: 0,
    }),
  };

  const transition = {
    x: { type: "spring" as const, stiffness: 300, damping: 30 },
    opacity: { duration: 0.2 },
  };

  return (
    <div className="h-screen flex relative overflow-hidden">
      {/* Floating Toggle */}
      <AuthToggle isLogin={isLogin} onToggle={toggleAuth} />

      <AnimatePresence mode="wait" custom={isLogin ? 1 : -1}>
        {isLogin ? (
          <motion.div
            key="login"
            custom={1}
            variants={slideVariants}
            initial="enter"
            animate="center"
            exit="exit"
            transition={transition}
            className="h-screen flex w-full"
          >
            <div className="flex-1 flex items-center justify-center p-8 bg-gradient-to-br from-slate-50 to-cyan-50">
              <LoginForm onSwitchToRegister={switchToRegister} />
            </div>
            <div className="flex-1 bg-gradient-to-br from-cyan-600 via-cyan-700 to-teal-700 relative overflow-hidden">
              <div className="absolute top-0 right-0 w-96 h-96 bg-white/10 rounded-full -translate-y-48 translate-x-48"></div>
              <div className="absolute bottom-0 left-0 w-72 h-72 bg-white/5 rounded-full translate-y-36 -translate-x-36"></div>
              <div className="absolute top-1/2 right-1/4 w-32 h-32 bg-white/10 rounded-full"></div>
              <AuthContent isLogin={isLogin} />
            </div>
          </motion.div>
        ) : (
          <motion.div
            key="register"
            custom={-1}
            variants={slideVariants}
            initial="enter"
            animate="center"
            exit="exit"
            transition={transition}
            className="h-screen flex w-full"
          >
            <div className="flex-1 bg-gradient-to-br from-cyan-600 via-cyan-700 to-teal-700 relative overflow-hidden">
              <div className="absolute top-0 left-0 w-96 h-96 bg-white/10 rounded-full -translate-y-48 -translate-x-48"></div>
              <div className="absolute bottom-0 right-0 w-72 h-72 bg-white/5 rounded-full translate-y-36 translate-x-36"></div>
              <div className="absolute top-1/3 left-1/4 w-24 h-24 bg-white/10 rounded-full"></div>
              <AuthContent isLogin={isLogin} />
            </div>
            <div className="flex-1 flex items-center justify-center p-8 bg-gradient-to-br from-slate-50 to-cyan-50">
              <RegisterForm onSwitchToLogin={switchToLogin} />
            </div>
          </motion.div>
        )}
      </AnimatePresence>
    </div>
  );
}
