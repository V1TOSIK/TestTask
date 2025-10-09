document.addEventListener("alpine:init", () => {
  Alpine.data("updateModal", () => ({
        updateForm: { },

        close() {
            Alpine.store('globalState').closeUpdateModal();
            this.updateForm = { };
        },

        getActiveTab(){
          return Alpine.store('globalState').activeTab;  
        },

        async submit() {
            try {
                const store = Alpine.store('globalState');
                const token = store.currentUser.token;

                const modelId = store.updateEntityId;

                if (!modelId) return;

                if (this.getActiveTab() === 'hotels')
                    this.updateForm.HotelId = modelId;
                else if (this.getActiveTab() === 'rooms')
                    this.updateForm.RoomId = modelId;

                const url = this.getActiveTab() === 'hotels'
                    ? `http://localhost:8000/api/hotels/${this.updateForm.HotelId}`
                    : `http://localhost:8000/api/rooms/${this.updateForm.RoomId}`;

                const res = await fetch(url, {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    body: JSON.stringify(this.updateForm)
                });

                if (!res.ok) throw new Error(await res.text());
                const data = await res.json();

                if (data.IsFailure) {
                    alert(data.Error);
                } else {
                    alert('Updated successfully!');
                }

                if (this.getActiveTab() === 'hotels') await store.fetchHotels();
                else await store.fetchRooms();

                this.close();
            } catch (err) {
                alert('Error: ' + err.message);
            }
        },
    }));
});