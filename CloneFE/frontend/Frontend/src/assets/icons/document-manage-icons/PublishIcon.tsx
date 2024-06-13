const PublishIcon = ({ ...props }: React.HTMLAttributes<SVGSVGElement>) => {
  return (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <g clipPath="url(#clip0_184_16043)">
        <path
          d="M5 4H19V6H5V4ZM5 14H9V20H15V14H19L12 7L5 14ZM13 12V18H11V12H9.83L12 9.83L14.17 12H13Z"
          fill="currentColor"
        />
      </g>
      <defs>
        <clipPath id="clip0_184_16043">
          <rect width="24" height="24" fill="white" />
        </clipPath>
      </defs>
    </svg>
  );
};

export default PublishIcon;
