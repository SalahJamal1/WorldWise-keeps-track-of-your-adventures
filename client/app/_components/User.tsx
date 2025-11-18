'use client';
import Image from 'next/image';
import { useAuth } from '../_hooks/useAuth';
import { logout } from '../_lib/apiAuth';
import { useRouter } from 'next/navigation';

export default function User() {
  const { user, dispatch } = useAuth()!;
  const router = useRouter();
  const onClick = async () => {
    dispatch({ type: 'USER_PENDING' });
    try {
      await logout();
      dispatch({ type: 'USER_LOGOUT' });
      router.push('/');
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <div className="bg-dark--2 flex items-center justify-between gap-5 px-3 py-4 absolute top-3 right-3 z-999 rounded-[5px] shadow-xl">
      <Image height={35} width={35} src="/user.svg" alt="name" />
      <h2 className="text-base font-semibold">
        Welcome, {user?.firstName?.toUpperCase()}
      </h2>
      <button
        onClick={onClick}
        className="bg-dark--1 px-2 py-1 rounded-[5px] uppercase duration-300 transition-all hover:bg-light--1 cursor-pointer font-semibold"
      >
        logout
      </button>
    </div>
  );
}
