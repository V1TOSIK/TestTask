document.addEventListener("alpine:init", () => {
  Alpine.data("pagination", (entity) => ({
    get page() {
      return Alpine.store('globalState')[`${entity}Pagination`].page;
    },
    get totalPages() {
      return Alpine.store('globalState').totalPages(entity);
    },

    prev() {
      const store = Alpine.store('globalState');
      if (this.page > 1)
        store.setPage(entity, this.page - 1);
    },

    next() {
      const store = Alpine.store('globalState');
      if (this.page < this.totalPages)
        store.setPage(entity, this.page + 1);
    },

    setPage(i) {
      const store = Alpine.store('globalState');
      store.setPage(entity, i);
    },
  }));
});
