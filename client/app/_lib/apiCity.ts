"use client";
import axios from "axios";
import { ICITY } from "../utils/ICITY";
import { api } from "./apiAuth";

export async function apiCity(lat: string, lng: string, signal?: AbortSignal) {
  const res = await axios.get(
    `https://api.bigdatacloud.net/data/reverse-geocode?latitude=${lat}&longitude=${lng}&key=bdc_50bea10fea6f45f9821d34f53843c7ca`,
    { signal }
  );
  return res;
}

export async function AddCity(data: ICITY) {
  const res = await api.post("/cities", data);
  return res;
}
export async function GetCities(signal?: AbortSignal) {
  const res = await api.get("/cities", { signal });
  return res;
}
export async function GetCity(id: number, signal?: AbortSignal) {
  const res = await api.get(`/cities/${id}`, { signal });
  return res;
}
export async function deleteCity(id: number) {
  const res = await api.delete(`/cities/${id}`);
  return res;
}
