import { useSearchParams } from 'next/navigation';

export function usePosition() {
  const searchParams = useSearchParams();
  const lat: string | null = searchParams.get('lat');
  const lng: string | null = searchParams.get('lng');
  return { lat, lng };
}
