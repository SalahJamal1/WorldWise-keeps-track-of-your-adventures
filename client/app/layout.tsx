import type { Metadata } from 'next';
import { Geist } from 'next/font/google';
import './globals.css';
import { Suspense } from 'react';
import Loading from './loading';
import { Toaster } from 'react-hot-toast';
import AuthContext from './_context/AuthContext';

const geistSans = Geist({
  variable: '--font-geist-sans',
  subsets: ['latin'],
});

export const metadata: Metadata = {
  title: 'WorldWise // keeps track of your adventures',
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`${geistSans.className} antialiased`}
        suppressHydrationWarning
      >
        <Suspense fallback={<Loading />}>
          <Toaster
            position="top-center"
            reverseOrder={false}
            gutter={8}
            containerClassName=""
            containerStyle={{}}
            toasterId="default"
            toastOptions={{
              // Define default options
              className: '',
              duration: 5000,
              removeDelay: 1000,
              style: {
                background: '#363636',
                color: '#fff',
              },

              error: {
                duration: 3000,
                style: {
                  textAlign: 'center',
                },
                iconTheme: {
                  primary: 'red',
                  secondary: 'black',
                },
              },

              // Default options for specific types
              success: {
                duration: 3000,
                style: {
                  textAlign: 'center',
                },
                iconTheme: {
                  primary: 'green',
                  secondary: 'black',
                },
              },
            }}
          />
          <AuthContext>{children}</AuthContext>
        </Suspense>
      </body>
    </html>
  );
}
