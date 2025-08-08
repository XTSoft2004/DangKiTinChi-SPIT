"use server";

import globalConfig from "@/app.config";
import { ILoginRequest, ILoginResponse, IRegisterRequest } from "@/types/auth";
import { IShowResponse } from "@/types/global";
import { cookies, headers } from "next/headers";

export const loginAccount = async (formData: ILoginRequest) => {
  const response = await fetch(`${globalConfig.baseUrl}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      // Authorization: `Bearer ${(await cookies()).get("refreshToken")?.value}`,
    },
    body: JSON.stringify(formData),
  });

  const data = await response.json();

  if (response.ok) {
    const res = data.data as ILoginResponse;

    if (res) {
      (await cookies()).set("accessToken", res.accessToken);
      (await cookies()).set("refreshToken", res.refreshToken);
    }
  }

  return {
    ok: response.ok,
    status: response.status,
    ...data,
  } as IShowResponse<ILoginResponse>;
};

export const register = async (formData: IRegisterRequest) => {
  const response = await fetch(`${globalConfig.baseUrl}/auth/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  });

  const data = await response.json();

  return {
    ok: response.ok,
    status: response.status,
    ...data,
  } as IShowResponse<ILoginResponse>;
};

export const logout = async () => {
  const response = await fetch(`${globalConfig.baseUrl}/auth/logout`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${(await cookies()).get("accessToken")?.value}`,
    },
  });

  if (response.ok) {
    (await cookies()).delete("accessToken");
    (await cookies()).delete("refreshToken");
  }

  return {
    ok: response.ok,
    status: response.status,
  };
};

export const refreshToken = async () => {
  const response = await fetch(`${globalConfig.baseUrl}/auth/refresh-token`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${(await cookies()).get("accessToken")?.value}`,
    },
  });

  const data = await response.json();

  if (response.ok) {
    const res = data.data as ILoginResponse;

    if (res) {
      (await cookies()).set("accessToken", res.accessToken);
      (await cookies()).set("refreshToken", res.refreshToken);
    }
  }

  return {
    ok: response.ok,
    status: response.status,
    ...data,
  } as IShowResponse<ILoginResponse>;
};
