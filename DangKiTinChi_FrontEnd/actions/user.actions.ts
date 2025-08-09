"use server";

import globalConfig from "@/app.config";
import { IBaseResponse, IIndexResponse, IShowResponse } from "@/types/global";
import { IUserRequest, IUserResponse, IUserUpdateRequest } from "@/types/user";
import next from "next";
import { revalidateTag } from "next/cache";
import { cookies, headers } from "next/headers";

export const getUsers = async (
    search: string = '',
    pageNumber: number = 1,
    pageSize: number = 10,) => {
    const response = await fetch(
        `${globalConfig.baseUrl}/user?search=${search}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
        {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${(await cookies()).get("accessToken")?.value}`
            },
            next: {
                tags: ['user.index'],
            },
        },
    )

    const data = await response.json()

    console.log(data);
    return {
        ok: response.ok,
        status: response.status,
        ...data,
    } as IIndexResponse<IUserResponse>
}

export const createUser = async (userRequest: IUserRequest) => {
    const response = await fetch(`${globalConfig.baseUrl}/user`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${(await cookies()).get("accessToken")?.value}`
        },
        body: JSON.stringify(userRequest)
    });

    const data = await response.json()
    revalidateTag('user.index')

    return {
        ok: response.ok,
        status: response.status,
        ...data,
    } as IBaseResponse
};

export const updateUser = async (userId: string, userUpdateRequest: IUserUpdateRequest) => {
    const response = await fetch(`${globalConfig.baseUrl}/user/${userId}`, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${(await cookies()).get("accessToken")?.value}`
        },
        body: JSON.stringify(userUpdateRequest)
    });

    const data = await response.json()
    revalidateTag('user.index')

    return {
        ok: response.ok,
        status: response.status,
        ...data,
    } as IBaseResponse
};

export const deleteUser = async (userId: string) => {
    const response = await fetch(`${globalConfig.baseUrl}/user/${userId}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${(await cookies()).get("accessToken")?.value}`
        }
    });

    const data = await response.json()
    revalidateTag('user.index')

    return {
        ok: response.ok,
        status: response.status,
        ...data,
    } as IBaseResponse
};