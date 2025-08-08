export interface ILoginRequest {
  userName: string;
  password: string;
  deviceId: string;
}

export interface ILoginResponse {
  id: number;
  userName: string;
  fullname: string;
  roleName: string;
  avatarUrl?: string;
  isLocked: boolean;
  accessToken: string;
  expiresAt: DateTime;
  refreshToken: string;
  refreshExpiresAt: DateTime;
}

export interface IRegisterRequest {
  userName: string;
  password: string;
  fullName: string;
}