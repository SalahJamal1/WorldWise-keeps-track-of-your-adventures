'use client';
import { ReactNode, useEffect, useState } from 'react';
import { useAuth } from '../_hooks/useAuth';
import { useRouter } from 'next/navigation';
import Loading from '../loading';

type Props = {
  children: ReactNode;
};

export default function ProtectPage({ children }: Props) {
  const [mount, setMount] = useState<boolean>(false);
  const { auth, loader } = useAuth()!;
  const router = useRouter();
  useEffect(() => {
    if (auth === undefined) return;
    if (!auth && !loader) {
      router.push('/login');
    }

    // eslint-disable-next-line react-hooks/set-state-in-effect
    setMount(true);
  }, [auth, loader, router]);
  if (!mount || loader) return <Loading />;
  return auth ? children : null;
}
