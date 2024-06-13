// Strongly type the environment variables when being loaded through the ".env" file
// Access the variables through import.meta.env
interface ImportMetaEnv {
	VITE_API_HOST: string;
	VITE_API_PORT: number;
}
