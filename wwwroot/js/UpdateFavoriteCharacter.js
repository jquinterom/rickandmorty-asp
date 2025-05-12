function saveFavorite(characterId, url) {
  $.ajax({
    url: url,
    type: "POST",
    data: { characterId: characterId },
    success: function () {
      const buttonIcon = $("#toggle-favorite-" + characterId).children("i");

      const buttonHasClass = buttonIcon.hasClass("far");

      if (buttonHasClass) {
        buttonIcon.removeClass("far");
        buttonIcon.addClass("fas text-warning");
      } else {
        buttonIcon.removeClass("fas text-warning");
        buttonIcon.addClass("far");
      }
    },
    error: function (error) {
      console.error("Error update favorite character", error);
    },
  });
}
