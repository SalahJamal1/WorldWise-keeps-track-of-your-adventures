'use client';
import { SubmitHandler, useForm } from 'react-hook-form';
import Logo from '../_components/Logo';
import { signup } from '../_lib/apiAuth';
import { useRouter } from 'next/navigation';
import toast from 'react-hot-toast';
import Message from '../_components/Message';

type FormValues = {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  password: string;
  confirmPassword: string;
};
export default function Page() {
  const labelStyle: string =
    'block font-semibold text-base tracking-wider mb-2 text-light--2 mb-1';
  const inputStyle: string =
    'block bg-light--3 text-lg placeholder:text-light--1 text-black px-3 py-2 rounded-[5px] outline-none focus:bg-white transition-all duration-300 w-full';
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>();
  const router = useRouter();
  const onSubmit: SubmitHandler<FormValues> = async data => {
    try {
      const res = await signup(data);
      toast.success('You have been registered successfully');
      router.push('/login');
    } catch (err: any) {
      const message =
        err?.response?.data?.message ||
        err?.response?.data?.errors?.ConfirmPassword[0] ||
        err?.message ||
        'Something went wrong';
      toast.error(message);
    }
  };
  return (
    <div className="bg-dark--1 h-[calc(100vh-3rem)]">
      <section className="max-w-3xl mx-auto pt-15">
        <div className="flex flex-col items-center space-y-10 mb-10">
          <Logo />

          <h2 className="text-center text-2xl font-medium tracking-wider">
            Sign up to explore a better experience!
          </h2>
        </div>
        <form
          onSubmit={handleSubmit(onSubmit)}
          className="bg-dark--2 grid grid-cols-2 gap-x-8 gap-y-6 px-10 py-16 rounded-[10px]"
        >
          <div>
            <label htmlFor="firstName" className={labelStyle}>
              First Name
            </label>

            <input
              type="text"
              id="firstName"
              {...register('firstName')}
              placeholder="Jack"
              className={inputStyle}
              required
            />
          </div>

          <div>
            <label htmlFor="lastName" className={labelStyle}>
              Last Name
            </label>

            <input
              type="text"
              id="lastName"
              {...register('lastName')}
              placeholder="Smith"
              className={inputStyle}
            />
          </div>

          <div>
            <label htmlFor="email" className={labelStyle}>
              Email Address
            </label>

            <input
              type="email"
              id="email"
              {...register('email')}
              placeholder="jack@example.com"
              className={inputStyle}
              required
            />
          </div>
          <div>
            <label htmlFor="phone" className={labelStyle}>
              Phone Number
            </label>

            <input
              type="text"
              id="phone"
              {...register('phoneNumber')}
              placeholder="079000000"
              className={inputStyle}
              required
            />
          </div>

          <div>
            <label htmlFor="password" className={labelStyle}>
              Password
            </label>

            <input
              type="password"
              id="password"
              {...register('password')}
              placeholder="••••••••"
              className={inputStyle}
              required
            />
          </div>

          <div>
            <label htmlFor="confirmPassword" className={labelStyle}>
              Confirm Password
            </label>

            <input
              type="password"
              id="confirmPassword"
              {...register('confirmPassword')}
              placeholder="••••••••"
              className={inputStyle}
              required
            />
          </div>

          <button
            type="submit"
            className="bg-brand--2 col-span-2 rounded-[5px] py-3 text-xl font-semibold text-white hover:opacity-90 transition cursor-pointer"
          >
            Sign Up
          </button>
        </form>
      </section>
    </div>
  );
}
