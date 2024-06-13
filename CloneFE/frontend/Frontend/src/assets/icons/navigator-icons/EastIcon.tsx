import React from "react";

const EastIcon = ({ ...props }: React.HTMLAttributes<SVGSVGElement>) => {
  return (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <g clipPath="url(#clip0_184_16014)">
        <path
          d="M15 5L13.59 6.41L18.17 11H2V13H18.17L13.58 17.59L15 19L22 12L15 5Z"
          fill="currentColor"
        />
      </g>
      <defs>
        <clipPath id="clip0_184_16014">
          <rect width="24" height="24" fill="white" />
        </clipPath>
      </defs>
    </svg>
  );
};

export default EastIcon;
