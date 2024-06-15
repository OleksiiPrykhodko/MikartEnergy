FROM node:18-alpine
WORKDIR /app
EXPOSE 4200

COPY package.json package-lock.json ./
RUN npm ci

COPY . .
RUN npm run ng build --configuration=production

CMD ["npm", "start"]