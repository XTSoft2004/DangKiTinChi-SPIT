'use client';

import { toast } from "sonner";

interface ToastOptions {
    title: string;
    description?: string;
    duration?: number;
}

class ToastHelperClass {
    success({ title, description, duration = 2000 }: ToastOptions) {
        toast.success(title, {
            description,
            duration,
        });
    }

    error({ title, description, duration = 2000 }: ToastOptions) {
        toast.error(title, {
            description,
            duration,
        });
    }

    info({ title, description, duration = 2000 }: ToastOptions) {
        toast.info(title, {
            description,
            duration,
        });
    }

    loading({ title, description, duration = 2000 }: ToastOptions) {
        const toastId = toast.loading(title, {
            description,
            duration,
        });

        setTimeout(() => {
            toast.dismiss(toastId);
        }, duration);

        return toastId;
    }
}

// 👉 Export singleton
export const ToastHelper = new ToastHelperClass();
