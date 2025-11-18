import { IWORKOUT } from '../utils/IWORKOUT';
import { api } from './apiAuth';

export async function deleteWorkouts(id: number) {
  const res = await api.delete(`/workouts/${id}`);
  return res;
}
export async function AddWorkouts(data: IWORKOUT) {
  const res = await api.post('/workouts', data);
  return res;
}
export async function GetWorkouts(signal?: AbortSignal) {
  const res = await api.get('/workouts', { signal });
  return res;
}
