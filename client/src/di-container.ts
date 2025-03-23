// di-container.ts
class DIContainer {
  private services = new Map<string, any>();

  register(name: string, instance: any): void {
    this.services.set(name, instance);
  }

  resolve<T>(name: string): T {
    const service = this.services.get(name);
    if (!service) {
      throw new Error(`Service ${name} not found`);
    }
    return service;
  }
}

const container = new DIContainer();
export default container;
