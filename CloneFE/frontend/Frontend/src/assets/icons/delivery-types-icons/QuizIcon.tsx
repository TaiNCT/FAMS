const QuizIcon = ({ ...props }: React.HTMLAttributes<SVGSVGElement>) => {
  return (
    <svg
      width="24"
      height="24"
      viewBox="0 0 24 24"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <g clipPath="url(#clip0_184_16181)">
        <path
          fillRule="evenodd"
          clipRule="evenodd"
          d="M20 3H4C2.9 3 2 3.9 2 5V19C2 20.1 2.9 21 4 21H20C21.1 21 22 20.1 22 19V5C22 3.9 21.1 3 20 3ZM20 19H4V5H20V19Z"
          fill="currentColor"
        />
        <path
          fillRule="evenodd"
          clipRule="evenodd"
          d="M19.41 10.42L17.99 9L14.82 12.17L13.41 10.75L12 12.16L14.82 15L19.41 10.42Z"
          fill="currentColor"
        />
        <path d="M10 7H5V9H10V7Z" fill="currentColor" />
        <path d="M10 11H5V13H10V11Z" fill="currentColor" />
        <path d="M10 15H5V17H10V15Z" fill="currentColor" />
      </g>
      <defs>
        <clipPath id="clip0_184_16181">
          <rect width="24" height="24" fill="white" />
        </clipPath>
      </defs>
    </svg>
  );
};

export default QuizIcon;