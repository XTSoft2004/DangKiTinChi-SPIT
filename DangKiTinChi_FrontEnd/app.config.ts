interface IGlobalConfig {
  baseUrl: string
  version?: string
}

const globalConfig: IGlobalConfig = {
    baseUrl: `${process.env.NEXT_PUBLIC_API_HOST || 'http://localhost:5050'}`,
}

export default globalConfig