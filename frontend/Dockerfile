# STAGE 1: Build.
FROM node:18-alpine as builder
WORKDIR /app

COPY package.json package-lock.json ./
# Clean Install of npm.
RUN npm ci
# Copy all (without files in .dockerignore) to the container. 
COPY . .
# Application building in production mode. 
RUN npm run ng build --configuration=production

# STAGE 2: Run in NGINX.
FROM nginx:stable
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=builder /app/dist/mikart-energy /usr/share/nginx/html

EXPOSE 4200