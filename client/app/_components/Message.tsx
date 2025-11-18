import React from 'react';

type Props = {
  message: string;
};

export default function Message({ message }: Props) {
  return (
    <p className="text-red-600 text-base font-semibold text-end col-2">
      {message}
    </p>
  );
}
