import React from "react";

const FirstPageIcon = ({ ...props }: React.HTMLAttributes<SVGSVGElement>) => {
  return (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <g clipPath="url(#clip0_184_16006)">
        <path
          d="M18.41 16.59L13.82 12L18.41 7.41L17 6L11 12L17 18L18.41 16.59ZM6 6H8V18H6V6Z"
          fill="currentColor"
        />
      </g>
      <defs>
        <clipPath id="clip0_184_16006">
          <rect width="24" height="24" fill="white" />
        </clipPath>
      </defs>
    </svg>
  );
};

export default FirstPageIcon;
