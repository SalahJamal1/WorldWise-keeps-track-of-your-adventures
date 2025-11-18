import axios, { InternalAxiosRequestConfig } from "axios";
import { FormValues } from "../login/page";

export const api = axios.create({
  baseURL:
    process.env.NEXT_PUBLIC_ENVIRONMENT === "docker"
      ? process.env.NEXT_PUBLIC_API_URL
      : "http://localhost:8080/api/v1",
  withCredentials: true,
  headers: {
    "Cache-Control": "no-cache, no-store, must-revalidate",
    Pragma: "no-cache",
    Expires: "0",
  },
});

api.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    if (typeof window !== "undefined") {
      if (config.headers) {
        const token = localStorage.getItem("jwt");
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
      }
    }
    return config;
  },
  (error: any) => Promise.reject(error)
);

export const login = async (data: FormValues) => {
  const res = await api.post("/auth/login", data);
  return res;
};
export const signup = async (data: FormValues) => {
  const res = await api.post("/auth/register", data);
  return res;
};
export const refresh = async () => {
  const res = await api.post("/auth/refresh-token");
  return res;
};
export const logout = async () => {
  const res = await api.get("/auth/logout");
  return res;
};
