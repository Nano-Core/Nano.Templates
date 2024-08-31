# Todo - Update and use NODE_JS_VERSION
ARG NODE_JS_VERSION

FROM node:$NODE_JS_VERSION
EXPOSE 8080

WORKDIR /app
COPY package*.json ./

RUN npm install 

COPY . .

RUN npm run build

CMD ["npm", "run", "start"]



