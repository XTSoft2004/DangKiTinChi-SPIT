export interface IUserResponse {
  id: number;
  userName: string;
  fullName: string;
  money: number;
  roleName: string;
}

export interface IUserRequest {
  userName: string;
  password: string;
  fullName: string;
}

export interface IUserUpdateRequest {
  password: string;
  fullName: string;
}