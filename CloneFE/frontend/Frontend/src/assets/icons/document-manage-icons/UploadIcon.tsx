const UploadIcon = ({ ...props }: React.HTMLAttributes<SVGSVGElement>) => {
  return (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <g clipPath="url(#clip0_184_16027)">
        <path
          d="M9 16H15V10H19L12 3L5 10H9V16ZM12 5.83L14.17 8H13V14H11V8H9.83L12 5.83ZM5 18H19V20H5V18Z"
          fill="currentColor"
        />
      </g>
      <defs>
        <clipPath id="clip0_184_16027">
          <rect width="24" height="24" fill="white" />
        </clipPath>
      </defs>
    </svg>
  );
};

export default UploadIcon;
