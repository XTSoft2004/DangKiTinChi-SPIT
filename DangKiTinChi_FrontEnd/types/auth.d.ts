export interface ILogin {
  userName: string;
  password: string;
  deviceId: string;
}

export interface ILoginResponse {
  id: number;
  username: string;
  fullname: string;
  roleName: string;
  avatarUrl?: string;
  isLocked: boolean;
  accessToken: string;
  expiresAt: DateTime;
  refreshToken: string;
  refreshExpiresAt: DateTime;
}

export interface IRegister {
  userName: string;
  password: string;
  fullName: string;
}