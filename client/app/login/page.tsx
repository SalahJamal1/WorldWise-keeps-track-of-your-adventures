'use client';
import Link from 'next/link';
import Logo from '../_components/Logo';

import { SubmitHandler, useForm } from 'react-hook-form';
import toast from 'react-hot-toast';
import { login } from '../_lib/apiAuth';
import { useAuth } from '../_hooks/useAuth';
import { useRouter } from 'next/navigation';
import Message from '../_components/Message';

export type FormValues = { email: string; password: string };
export default function Page() {
  const { dispatch } = useAuth()!;

  const labelStyle: string = 'font-semibold text-base tracking-wider';
  const inputStyle: string =
    'bg-light--3 text-xl placeholder:text-light--1 text-black px-3 py-2 rounded-[5px] outline-0 border-0 focus:bg-white duration-300 transition-all mb-5 col-span-2';
  const router = useRouter();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>();

  const onSubmit: SubmitHandler<FormValues> = async data => {
    console.log(data);
    try {
      const res = await login(data);
      localStorage.setItem('jwt', res.data.accessToken);
      dispatch({ type: 'USER_LOGIN', payload: res.data.user });
      router.push('/apps');
      toast.success('You have logged in successfully');
    } catch (err) {
      if (err instanceof Error) {
        const message: string =
          (err as unknown as { response: { data: { message: string } } })
            ?.response?.data?.message || err.message;
        toast.error(message || err.message);
      }
    }
  };

  return (
    <div className="bg-dark--1 h-[calc(100vh-3rem)]">
      <section className="max-w-3xl mx-auto pt-15">
        <div className="flex flex-col items-center space-y-10 mb-10">
          <Logo />

          <h2 className="text-center text-2xl font-medium tracking-wider">
            Log in to explore a better experience!
          </h2>
        </div>
        <form
          onSubmit={handleSubmit(onSubmit)}
          className="bg-dark--2 grid grid-cols-1 px-10 py-16 rounded-[10px] space-y-2"
        >
          <label className={labelStyle} htmlFor="email">
            Email address
          </label>
          <input
            type="email"
            placeholder="jack@example.com"
            id="email"
            {...register('email')}
            className={inputStyle}
            required
          />
          <label className={labelStyle} htmlFor="password">
            Password
          </label>

          <input
            type="password"
            id="password"
            {...register('password')}
            className={inputStyle}
            placeholder="••••••••"
            min={8}
            required
          />
          <div className="flex items-center justify-between col-span-2">
            <button
              type="submit"
              className="bg-brand--2 rounded-[5px] border-none py-2 px-12 text-xl cursor-pointer hover:opacity-90 transition"
            >
              Login
            </button>
            <Link
              href="/signup"
              className="text-base border-b-2 border-brand--2 pb-2 font-medium duration-150 transition-all hover:border-transparent"
            >
              Create new Account
            </Link>
          </div>
        </form>
      </section>
    </div>
  );
}
