FROM node:20.11.1

WORKDIR /app

# Grab the environment variables
ARG VITE_API_HOST
ARG VITE_API_PORT
ENV VITE_API_PORT=$VITE_API_PORT
ENV VITE_API_HOST=$VITE_API_HOST

# Copy source code into the image
COPY . .

# Install dependencies
WORKDIR /app

# Adding environment variable to .env.production
RUN echo "VITE_API_HOST=${VITE_API_HOST}" > .env.production
RUN echo "VITE_API_PORT=${VITE_API_PORT}" >> .env.production

RUN npm install
RUN npm install -g serve
RUN npm run build

# Running the built project
CMD ["serve", "-s", "./dist", "-p", "8000"]