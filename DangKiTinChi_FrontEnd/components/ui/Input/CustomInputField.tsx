'use client';

import {
    Controller,
    useFormContext,
    FieldValues,
    Path,
    RegisterOptions,
    ControllerRenderProps,
} from 'react-hook-form';

import { Input } from '@/components/ui/shadcn-ui/input';
import { Label } from '@/components/ui/shadcn-ui/label';
import { Eye, EyeOff } from 'lucide-react';
import { useState, ReactNode } from 'react';

interface CustomInputFieldProps<T extends FieldValues> {
    name: Path<T>;
    label: string;
    type?: 'text' | 'password';
    placeholder?: string;
    icon?: ReactNode;
    rules?: RegisterOptions<T, Path<T>>;
}

export function CustomInputField<T extends FieldValues>({
    name,
    label,
    type = 'text',
    placeholder,
    icon,
    rules = {},
}: CustomInputFieldProps<T>) {
    const {
        control,
        formState: { errors },
    } = useFormContext<T>();

    const error = errors[name]?.message as string | undefined;
    const [showPassword, setShowPassword] = useState(false);

    const renderInput = (field: ControllerRenderProps<T, Path<T>>) => {
        const inputType = type === 'password' ? (showPassword ? 'text' : 'password') : type;

        const getInputClasses = () => {
            let baseClasses =
                'h-10 sm:h-11 md:h-12 text-sm sm:text-base border-2 rounded-lg transition-all duration-200 text-black w-full';

            if (icon) baseClasses += ' pl-9 sm:pl-10';
            else baseClasses += ' pl-3 sm:pl-4';

            if (type === 'password') baseClasses += ' pr-9 sm:pr-10';
            else baseClasses += ' pr-3 sm:pr-4';

            if (error)
                baseClasses += ' border-red-500 focus:border-red-500 focus:ring-red-500 focus:ring-1';
            else baseClasses += ' border-gray-300 focus:border-blue-500 focus:ring-blue-500 focus:ring-1';

            return baseClasses;
        };

        return (
            <div className="relative w-full">
                {icon && (
                    <div className="absolute left-3 sm:left-3.5 top-1/2 -translate-y-1/2 text-gray-500 z-10">
                        <div className="w-4 h-4 sm:w-5 sm:h-5 flex items-center justify-center">{icon}</div>
                    </div>
                )}
                <Input
                    id={name}
                    type={inputType}
                    placeholder={placeholder}
                    className={getInputClasses()}
                    {...field}
                />
                {type === 'password' && (
                    <button
                        type="button"
                        className="absolute right-3 sm:right-3.5 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-700 transition-colors z-10 p-1"
                        onClick={() => setShowPassword(!showPassword)}
                        aria-label={showPassword ? 'Ẩn mật khẩu' : 'Hiện mật khẩu'}
                    >
                        {showPassword ? (
                            <EyeOff className="w-4 h-4 sm:w-5 sm:h-5" />
                        ) : (
                            <Eye className="w-4 h-4 sm:w-5 sm:h-5" />
                        )}
                    </button>
                )}
            </div>
        );
    };

    return (
        <div className="w-full space-y-1.5 sm:space-y-2">
            <Label htmlFor={name} className="text-sm sm:text-base font-semibold text-slate-700 block">
                {label}
            </Label>

            <Controller
                name={name}
                control={control}
                rules={{
                    required: `${label} không được bỏ trống`,
                    ...rules,
                }}
                render={({ field }) => renderInput(field)}
            />

            {error && (
                <p className="text-red-500 text-xs sm:text-sm mt-1 leading-tight">
                    {error}
                </p>
            )}
        </div>
    );
}
