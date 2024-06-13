const ChromeReaderModeIcon = ({
  ...props
}: React.HTMLAttributes<SVGSVGElement>) => {
  return (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <g clipPath="url(#clip0_184_16237)">
        <path
          d="M21 4H3C1.9 4 1 4.9 1 6V19C1 20.1 1.9 21 3 21H21C22.1 21 23 20.1 23 19V6C23 4.9 22.1 4 21 4ZM3 19V6H11V19H3ZM21 19H13V6H21V19ZM14 9.5H20V11H14V9.5ZM14 12H20V13.5H14V12ZM14 14.5H20V16H14V14.5Z"
          fill="currentColor"
        />
      </g>
      <defs>
        <clipPath id="clip0_184_16237">
          <rect width="24" height="24" fill="white" />
        </clipPath>
      </defs>
    </svg>
  );
};

export default ChromeReaderModeIcon;
