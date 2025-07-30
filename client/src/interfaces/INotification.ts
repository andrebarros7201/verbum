interface INotification {
  type: "success" | "error" | null;
  message: string | null;
  isVisible: boolean;
}

export type { INotification };
