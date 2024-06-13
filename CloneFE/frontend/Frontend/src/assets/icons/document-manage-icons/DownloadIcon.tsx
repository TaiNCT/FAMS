const DownloadIcon = ({ ...props }: React.HTMLAttributes<SVGSVGElement>) => {
  return (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <g clipPath="url(#clip0_184_16023)">
        <path
          d="M19 9H15V3H9V9H5L12 16L19 9ZM11 11V5H13V11H14.17L12 13.17L9.83 11H11ZM5 18H19V20H5V18Z"
          fill="currentColor"
        />
      </g>
      <defs>
        <clipPath id="clip0_184_16023">
          <rect width="24" height="24" fill="white" />
        </clipPath>
      </defs>
    </svg>
  );
};

export default DownloadIcon;
